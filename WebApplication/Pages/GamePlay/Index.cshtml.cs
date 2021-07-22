using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BattleShipLogic;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Domain.Enums;
namespace WebApplication.Pages.GamePlay
{
    public class Index : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly DAL.AppDbContext _context;

        public Index(DAL.AppDbContext context, ILogger<IndexModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        public Game Game { get; set; } = default!;

        public Bot Bot { get; set; } = default!;
        
        [BindProperty]
        public BattleShipGame BattleShipGame { get; set; } = new BattleShipGame();

        public bool GameFinished { get; private set; }
        
        //[BindProperty(SupportsGet = true)]
        //public int PosX { get; set; } = 0;
        //[BindProperty(SupportsGet = true)]
        //public int PosY { get; set; } = 0;

        public async Task OnGetAsync(int id, int? x, int? y, string? bot, bool? init)
        {
                Game = await _context.Games
                .Where(g => g.GameId == id)
                .FirstOrDefaultAsync();
                
                BattleShipGame.SetGameFromDatabase(Game.GameId);
                
                if(init.HasValue)
                {
                    if (init.Value)
                    {
                        BattleShipGame.GenerateBoard();
                        List<BoardState> boards = new List<BoardState>();
                        for (int xx = 0; xx < BattleShipGame.GetLengthBoard(0); xx += 1) {
                            for (int yy = 0; yy < BattleShipGame.GetLengthBoard(1); yy += 1)
                            {
                                if(BattleShipGame.GetCellValue(xx, yy, 1) != ECellState.Empty || BattleShipGame.GetCellValue(xx, yy, 2) != ECellState.Empty)
                                {
                                    if (BattleShipGame.GetCellValue(xx, yy, 1) != ECellState.Empty)
                                    {
                                        boards.Add(new BoardState
                                        {
                                            ArrayIndexX = xx, 
                                            ArrayIndexY = yy, 
                                            Value = BattleShipGame.GetCellValue(xx,yy,1),
                                            Player = _context.Players.FirstOrDefault(p => p.PlayerId == Game.PlayerAId)!
                                        });
                                    }
                                    
                                    
                                    if (BattleShipGame.GetCellValue(xx, yy, 2) != ECellState.Empty)
                                    {
                                        boards.Add(new BoardState
                                        {
                                            ArrayIndexX = xx,
                                            ArrayIndexY = yy,
                                            Value = BattleShipGame.GetCellValue(xx,yy,2),
                                            Player = _context.Players.FirstOrDefault(p => p.PlayerId == Game.PlayerBId)!
                                        });
                                    }
                                }
                            }
                        }
                        
                        List<Ship> shipsA = new List<Ship>();
                        for (int i = 0; i < BattleShipGame.GetShip(1).Count; i++)
                        {
                            shipsA.Add(new Ship
                            {
                                Size = BattleShipGame.GetShipSize(BattleShipGame.GetShip(1)[i+1]),
                                Cells = BattleShipGame.GetShip(1)[i+1],
                                Player = _context.Players.FirstOrDefault(p => p.PlayerId == Game.PlayerAId)!
                            });
                        }
                        
                        List<Ship> shipsB = new List<Ship>();
                        for (int i = 0; i < BattleShipGame.GetShip(2).Count; i++)
                        {
                            shipsB.Add(new Ship
                            {
                                Size = BattleShipGame.GetShipSize(BattleShipGame.GetShip(2)[i+1]),
                                Cells = BattleShipGame.GetShip(2)[i+1],
                                Player = _context.Players.FirstOrDefault(p => p.PlayerId == Game.PlayerBId)!
                            });
                        }

                        if(_context.Players.FirstOrDefault(p => p.PlayerId == Game.PlayerBId) != null)
                        {
                            if (_context.Players.FirstOrDefault(p => p.PlayerId == Game.PlayerBId).EPlayerType ==
                                EPlayerType.AI)
                            {
                                var dbBot = new Bot
                                {
                                    PlayerId = Game.PlayerBId
                                };
                                await _context.Bots.AddAsync(dbBot);
                            }
                        }
                        
                        await _context.BoardStates.AddRangeAsync(boards);
                        await _context.Ships.AddRangeAsync(shipsA);
                        await _context.Ships.AddRangeAsync(shipsB);
                        await _context.SaveChangesAsync();
                        BattleShipGame.SetGameFromDatabase(Game.GameId);
                    }
                }
                GameFinished = BattleShipGame.CheckWinner();
            if(x.HasValue && y.HasValue)
            {
                var boardnum = BattleShipGame.NextMoveByP1 ? 2 : 1;
                if(BattleShipGame.GetCellValue(x.Value,y.Value,boardnum) == ECellState.Empty || BattleShipGame.GetCellValue(x.Value,y.Value,boardnum) == ECellState.Ship){
                    BattleShipGame.MakeAMove(x.Value, y.Value);
                    var boardstate = new BoardState
                    {
                        PlayerId = BattleShipGame.NextMoveByP1 ? Game.PlayerAId : Game.PlayerBId,
                        ArrayIndexX = x.Value,
                        ArrayIndexY = y.Value,
                        Value = BattleShipGame.GetCellValue(x.Value, y.Value, boardnum)
                    };
                    Game.ENextMoveAfterHit = BattleShipGame.NextMoveByP1 ? ENextMoveAfterHit.PlayerA : ENextMoveAfterHit.PlayerB;
                    await _context.BoardStates.AddAsync(boardstate);
                    _context.Games.Update(Game);
                    await _context.SaveChangesAsync();
                    BattleShipGame.SetGameFromDatabase(Game.GameId);
                    GameFinished = BattleShipGame.CheckWinner();
                }
            }

            if (!BattleShipGame.NextMoveByP1 && BattleShipGame.PlayerBType == EPlayerType.AI && !GameFinished)
            {
                Bot = await _context.Bots
                    .Where(g => g.PlayerId == Game.PlayerBId)
                    .FirstOrDefaultAsync();
                //var boardnum = BattleShipGame.NextMoveByP1 ? 2 : 1;
                var boardnum = 1;
                int xx;
                int yy;
                do
                {
                    (xx, yy) = BattleShipGame.BotMove();
                } while (xx == -1 && yy == -1);
                var boardstate = new BoardState
                {
                    PlayerId = BattleShipGame.NextMoveByP1 ? Game.PlayerAId : Game.PlayerBId,
                    ArrayIndexX = xx,
                    ArrayIndexY = yy,
                    Value = BattleShipGame.GetCellValue(xx, yy, boardnum)
                };
                if (BattleShipGame.BotFirstMove != null)
                {
                    Bot.BotFirstMoveX = (int) char.GetNumericValue(BattleShipGame.BotFirstMove[0]);
                    Bot.BotFirstMoveY = (int) char.GetNumericValue(BattleShipGame.BotFirstMove[2]);
                }
                if (BattleShipGame.BotLastShipHit != null)
                {
                    Bot.BotLastShipHitX = (int) char.GetNumericValue(BattleShipGame.BotLastShipHit[0]);
                    Bot.BotLastShipHitY = (int) char.GetNumericValue(BattleShipGame.BotLastShipHit[2]);
                }
                Bot.BotCurrentDirection = BattleShipGame.BotCurrentDirection;
                Game.ENextMoveAfterHit = BattleShipGame.NextMoveByP1 ? ENextMoveAfterHit.PlayerA : ENextMoveAfterHit.PlayerB;
                _context.BoardStates.Add(boardstate);
                _context.Games.Update(Game);
                _context.Bots.Update(Bot);
                await _context.SaveChangesAsync();
                BattleShipGame.SetGameFromDatabase(Game.GameId);
                GameFinished = BattleShipGame.CheckWinner();
            }
        }
    }
}