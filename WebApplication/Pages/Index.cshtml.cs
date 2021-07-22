using System;
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

namespace WebApplication.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly DAL.AppDbContext _context;

        public IndexModel(DAL.AppDbContext context, ILogger<IndexModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IList<Game> Game { get; set; }  = default!;
        public IList<Player> PlayersA { get; set; } = default!;
        public IList<Player> PlayersB { get; set; } = default!;
        
        [BindProperty] public string PlayerA { get; set; }  = default!;

        [BindProperty] public string PlayerB { get; set; }  = default!;
        
        [BindProperty] public string GameName { get; set; }  = default!;
        
        [BindProperty] public int BoardSize { get; set; }
        
        [BindProperty] public int BoatsCanTouch { get; set; }

        //[BindProperty] public int DeleteGameId { get; set; } = -1;
        
        public List<string> BoardSizesList = new List<string> { "10x10", "8x8", "6x6", "4x4" };


        public async Task OnGetAsync(int? id)
        {
            if (id.HasValue)
            {
                var dbGame = await _context.Games.Where(g => g.GameId == id).FirstOrDefaultAsync();
                var dbPlayerA = await _context.Players.Where(p => p.PlayerId == dbGame.PlayerAId).FirstOrDefaultAsync();
                var dbPlayerB = await _context.Players.Where(p => p.PlayerId == dbGame.PlayerBId).FirstOrDefaultAsync();
                var dbBot = await _context.Bots.FirstOrDefaultAsync(b => b.PlayerId == dbPlayerB.PlayerId);
                _context.BoardStates.RemoveRange(_context.BoardStates.Where(b=>b.PlayerId==dbPlayerA.PlayerId));
                _context.BoardStates.RemoveRange(_context.BoardStates.Where(b=>b.PlayerId==dbPlayerB.PlayerId));
                _context.Ships.RemoveRange(_context.Ships.Where(b=>b.PlayerId==dbPlayerA.PlayerId));
                _context.Ships.RemoveRange(_context.Ships.Where(b=>b.PlayerId==dbPlayerB.PlayerId));
                _context.Games.Remove(dbGame);
                await _context.SaveChangesAsync();
                _context.Players.Remove(dbPlayerA);
                _context.Players.Remove(dbPlayerB);
                _context.Bots.Remove(dbBot);
                await _context.SaveChangesAsync();
            }
            
            Game = await _context
                .Games.OrderBy(x => x.CreatedDate).ToListAsync();
            PlayersA = await _context.Players.Join(_context.Games,
                    p => p.PlayerId,
                    g => g.PlayerAId,
                    (p, g) => new Player
                    {
                        PlayerId = p.PlayerId,
                        Name=p.Name,
                        EPlayerType=p.EPlayerType,
                        GameId=p.GameId,
                        Game=p.Game
                    }).ToListAsync();

            PlayersB = await _context.Players.Join(_context.Games,
                p => p.PlayerId,
                g => g.PlayerBId,
                (p, g) => new Player
                {
                    PlayerId = p.PlayerId,
                    Name=p.Name,
                    EPlayerType=p.EPlayerType,
                    GameId=p.GameId,
                    Game=p.Game
                }).ToListAsync();
        }

        public async Task<IActionResult> OnPostNewGame()
        {
            var bS = 0;
            switch (BoardSize)
            {
                case 0:
                    bS = 10;
                    break;
                case 1:
                    bS = 8;
                    break;
                case 2:
                    bS = 6;
                    break;
                case 3:
                    bS = 4;
                    break;
                default:
                    bS = 10;
                    break;
            }

            var playerA = new Player()
            {
                Name = PlayerA,
                EPlayerType = EPlayerType.Human
            };
            var playerB = new Player()
            {
                Name = PlayerB,
                EPlayerType = EPlayerType.AI
            };
            var game = new Game()
            {
                Name = GameName,
                PlayerA = playerA,
                PlayerB = playerB,
                BoardHeight = bS,
                BoardWidth = bS,
                ENextMoveAfterHit = ENextMoveAfterHit.PlayerA,
                EBoatsCanTouch = EBoatsCanTouch.No
            };
            await _context.Games.AddAsync(game);
            await _context.SaveChangesAsync();
            playerA.GameId = game.GameId;
            playerB.GameId = game.GameId;
            //await _context.Players.AddAsync(playerA);
            //await _context.Players.AddAsync(playerB);
            await _context.SaveChangesAsync();

            return RedirectToPage("./GamePlay/Index", new {id = game.GameId, init = true});
        }
    }
}