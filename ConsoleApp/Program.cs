using System;
using System.Collections.Generic;
using System.Linq;
using BattleShipLogic;
using MenuSystem;
using DAL;
using Domain;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp
{
    internal static class Program
    {
        private static void Main()
        {
            /*
            Console.WriteLine("Deleting DB");
            db.Database.EnsureDeleted();
            Console.WriteLine("Migrating DB");
            db.Database.Migrate();
            Console.WriteLine("Adding data to DB");
            */
            Console.Write("============> ");
            Menu.WriteWithColor("TIC-TAC-TOE", "yellow");
            Menu.WriteWithColor("LW", "blue");
            Console.WriteLine("<=============");
            
            var menuB = new Menu(MenuLevel.Level2Plus);
            menuB.AddMenuItem(new MenuItem("Just a function", "1", DefaultMenuAction));
            menuB.AddMenuItem(new MenuItem("sayHi", "2", DefaultMenuAction));
            menuB.AddMenuItem(new MenuItem("Sub3b", "3", DefaultMenuAction));

            var menuA = new Menu(MenuLevel.Level1);
            
            menuA.AddMenuItem(new MenuItem("SubMenuB", "1", menuB.RunMenu));
            //menuA.MenuItems.Add(new MenuItem("Sub2", "2", DefaultMenuAction));
            //menuA.MenuItems.Add(new MenuItem("Sub3", "3", DefaultMenuAction));
            
            var menu = new Menu(MenuLevel.Level0);
            
            menu.AddMenuItem(new MenuItem("SubMenuA", "1", menuA.RunMenu));
            menu.AddMenuItem(new MenuItem("New game Player vs Player. START GAME", "2", 
                () => NewBattleShipGame()));
            menu.AddMenuItem(new MenuItem("New game Player vs BOT", "3", () => NewBattleShipGame(true)));
            menu.AddMenuItem(new MenuItem("Load Game from Cloud", "l", 
                () => LoadGameActionDatabase(new BattleShipGame(), true)));
            menu.AddMenuItem(new MenuItem("Load Game from local save file", "k", 
                () => LoadGameAction(new BattleShipGame(), true)));

            menu.RunMenu();
            
        }

        private static string DefaultMenuAction()
        {
            Console.WriteLine("Not implemented yet!");

            return "";
        }

        private static string GetText(int maxLength)
        {
            string res;
            do
            {
                res = Console.ReadLine()!;
                if (string.IsNullOrEmpty(res) || string.IsNullOrWhiteSpace(res) || res.Length > maxLength)
                {
                    Console.WriteLine("Wrong input! Please provide a proper name");
                    continue;
                }
                break;
            } while (true);

            return res;
        }
        
        private static string NewBattleShipGame(bool bot = false)
        {
            var game = new BattleShipGame();
            Console.WriteLine("How should we call you Player 1?");
            var playerA = GetText(64);
            Console.WriteLine(bot ? "How should we call BOT?" : "How should we call you Player 2?");
            var playerB = GetText(64);
            Console.WriteLine("What is the size of the board? Give Width (1-10), Height(1-10)");
            var boardSize = 10;
            do
            {
                Console.WriteLine("Please choose board size");
                Console.WriteLine("1 - 10x10");
                Console.WriteLine("2 - 8x8");
                Console.WriteLine("3 - 6x6");
                Console.WriteLine("4 - 4x4");
                    var boardSizeChoose = Console.ReadKey();
                if (int.Parse(boardSizeChoose.KeyChar.ToString()) <= 4 && int.Parse(boardSizeChoose.KeyChar.ToString()) >= 1)
                {
                    boardSize = int.Parse(boardSizeChoose.KeyChar.ToString()) switch
                    {
                        1 => 10,
                        2 => 8,
                        3 => 6,
                        4 => 4,
                        _ => boardSize
                    };
                    break;
                }
                
                Console.WriteLine("\nYou chose not existing option. Please, try again");
            } while (true);
            var canBoatsTouch = EBoatsCanTouch.No;
            do
            {
                Console.WriteLine("\nCan boats touch each other?");
                Console.WriteLine("1 - No");
                Console.WriteLine("2 - Yes");
                var boatsTouchChoose = Console.ReadKey();
                if (int.Parse(boatsTouchChoose.KeyChar.ToString()) <= 2 && int.Parse(boatsTouchChoose.KeyChar.ToString()) >= 1)
                {
                    canBoatsTouch = int.Parse(boatsTouchChoose.KeyChar.ToString()) switch
                    {
                        1 => EBoatsCanTouch.No,
                        2 => EBoatsCanTouch.Yes,
                        _ => canBoatsTouch
                    };
                    break;
                }
                Console.WriteLine("\nYou chose not existing option. Please, try again");
            } while (true);
            game.BattleShipGameInit(playerA!, playerB!, EPlayerType.Human, bot ? EPlayerType.AI : EPlayerType.Human,
                boardSize, boardSize, canBoatsTouch);
            game.GenerateBoard();
            return BattleShip(game);
        }
        
        private static string BattleShip(BattleShipGame game)
        {
            BattleShipUI.GameUI.DrawBoard(game.GetBoard1(), game.GetBoard2(), game.PlayerA, game.PlayerB);
            //string userChoice = "";
            string userChoice;
            do
            {
                var menu = new Menu(MenuLevel.Level1);
                if(!game.GameFinished)
                {
                    menu.AddMenuItem(new MenuItem(
                        $"Player {(game.NextMoveByP1 ? game.PlayerA : game.PlayerB)} make a move",
                        userChoice: "p",
                        () =>
                        {
                            if(game.PlayerBType == EPlayerType.AI && !game.NextMoveByP1)
                            {
                                int x;
                                int y;
                                do
                                {
                                    (x, y) = game.BotMove();
                                } while (x == -1 && y == -1);
                            }
                            else
                            {
                                var (x, y) = GetMoveCoordinates();
                                game.MakeAMove(x, y);
                            }
                            BattleShipUI.GameUI.DrawBoard(game.GetBoard1(), game.GetBoard2(), game.PlayerA, game.PlayerB);
                            return "redraw";
                        })
                    );
                
                    menu.AddMenuItem(new MenuItem(
                        "Save game",
                        userChoice: "s",
                        () => SaveGameAction(game))
                    );
                }
                menu.AddMenuItem(new MenuItem(
                    "Load game From Database",
                    userChoice: "l",
                    () => LoadGameActionDatabase(game))
                );
                menu.AddMenuItem(new MenuItem(
                    "Load local game save file",
                    userChoice: "k",
                    () => LoadGameAction(game))
                );
                userChoice = menu.RunMenu();
            } while (userChoice == "redraw");
            
            return userChoice;
        }

        static (int x, int y) GetMoveCoordinates()
        {
            char[] abc = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            Console.WriteLine("Upper left corner is (A1)!");
            Console.Write("Give Y (A-J), X (1-10). EXAMPLE C5:");
            do
            {
                var userValue = Console.ReadLine()?.Trim();

                if (userValue!.Length < 2 && userValue.Length > 3) { Console.WriteLine("Wrong Input. Please try again. INPUT EXAMPLE: D7"); continue;}
                var x = userValue.Length == 3 ? int.Parse(userValue![1].ToString() + userValue![2].ToString()) - 1 : int.Parse(userValue![1].ToString()) - 1;
                var y = Array.IndexOf(abc, userValue![0]);
                return (x, y);
            } while (true);
        }

        static string SaveGameAction(BattleShipGame game)
        {
            var defaultName = "save_" + DateTime.Now.ToString("yyyy-MM-dd") + ".json";
            Console.Write($"File name ({defaultName}):");
            var fileName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(fileName))
            {
                fileName = defaultName;
            }
            
            if (!fileName.Contains(".json"))
            {
                fileName += ".json";
            }

            var serializedGame = game.GetSerializedGameState();

            // Console.WriteLine(serializedGame);
            System.IO.File.WriteAllText($"D:\\{fileName}", serializedGame);

            var dbOptions = new DbContextOptionsBuilder<AppDbContext>().UseSqlServer(
                @"
                    Server=SAILEKEYEV; 
                    Database=csharp;
                    Trusted_Connection=True;
                    MultipleActiveResultSets=true"
            ).Options;
            
            using var db = new AppDbContext(dbOptions);
            //using var db = new AppDbContext();
            //db.Database.EnsureDeleted();
            db.Database.Migrate();

            var dbPlayerA = new Player
            {
                Name = game.PlayerA,
                EPlayerType = game.PlayerAType
            };
            
            var dbPlayerB = new Player
            {
                Name = game.PlayerB,
                EPlayerType = game.PlayerBType
            };
            
            List<BoardState> boards = new List<BoardState>();
            for (var x = 0; x < game.GetLengthBoard(0); x += 1) {
                for (var y = 0; y < game.GetLengthBoard(1); y += 1)
                {
                    if (game.GetCellValue(x, y, 1) == ECellState.Empty &&
                        game.GetCellValue(x, y, 2) == ECellState.Empty) continue;
                    if (game.GetCellValue(x, y, 1) != ECellState.Empty)
                    {
                        boards.Add(new BoardState
                        {
                            ArrayIndexX = x, 
                            ArrayIndexY = y, 
                            Value = game.GetCellValue(x,y,1),
                            Player = dbPlayerA
                        });
                    }
                        
                        
                    if (game.GetCellValue(x, y, 2) != ECellState.Empty)
                    {
                        boards.Add(new BoardState
                        {
                            ArrayIndexX = x,
                            ArrayIndexY = y,
                            Value = game.GetCellValue(x,y,2),
                            Player = dbPlayerB
                        });
                    }
                }
            }
            
            db.BoardStates.AddRange(boards);
            
            List<Ship> shipsA = new List<Ship>();
            for (var i = 0; i < game.GetShip(1).Count; i++)
            {
                shipsA.Add(new Ship
                {
                    Size = game.GetShipSize(game.GetShip(1)[i+1]),
                    Cells = game.GetShip(1)[i+1],
                    Player = dbPlayerA
                });
            }
            
            List<Ship> shipsB = new List<Ship>();
            for (var i = 0; i < game.GetShip(2).Count; i++)
            {
                shipsB.Add(new Ship
                {
                    Size = game.GetShipSize(game.GetShip(2)[i+1]),
                    Cells = game.GetShip(2)[i+1],
                    Player = dbPlayerB
                });
            }
            
            var dbGame = new Game
            {
                Name = fileName.Remove(fileName.Length-5),
                BoardHeight = game.BoardWidth,
                BoardWidth = game.BoardHeight,
                ENextMoveAfterHit = game.NextMoveByP1 ? ENextMoveAfterHit.PlayerA : ENextMoveAfterHit.PlayerB,
                PlayerA = dbPlayerA,
                PlayerB = dbPlayerB
            };
            
            if(game.PlayerBType == EPlayerType.AI)
            {
                var dbBot = new Bot
                {
                    PlayerId = dbGame.PlayerBId,
                    BotFirstMoveX = game.BotFirstMove!=null ? (int) char.GetNumericValue(game.BotFirstMove[0]) : -1,
                    BotFirstMoveY = game.BotFirstMove!=null ? (int) char.GetNumericValue(game.BotFirstMove[2]) : -1,
                    BotLastShipHitX = game.BotLastShipHit!=null ? (int) char.GetNumericValue(game.BotLastShipHit[0]) : -1,
                    BotLastShipHitY = game.BotLastShipHit!=null ? (int) char.GetNumericValue(game.BotLastShipHit[2]) : -1,
                    BotCurrentDirection = game.BotCurrentDirection
                };
                db.Bots.Add(dbBot);
            }
            
            db.Ships.AddRange(shipsA);
            db.Ships.AddRange(shipsB);
            
            //dbGame.PlayerA = dbPlayerA;
            //dbGame.PlayerB = dbPlayerB;

            db.Games.Add(dbGame);
            db.SaveChanges();

            dbPlayerA.GameId = dbGame.GameId;
            dbPlayerB.GameId = dbGame.GameId;
            //db.Players.Add(dbPlayerA);
            //db.Players.Add(dbPlayerB);
            db.SaveChanges();
            
            return "";
        }


        private static string LoadGameAction(BattleShipGame game, bool callFromMenu = false)
        {
            var files = System.IO.Directory.EnumerateFiles("D:\\", "*.json").ToList();
            if(files.Count == 0){ Console.WriteLine("There isn't any local save file"); return ""; }
            for (var i = 0; i < files.Count; i++)
            {
                Console.WriteLine($"{i} - {files[i]}");
            }

            var fileNo = Console.ReadKey();
            var fileName = files[int.Parse(fileNo.KeyChar.ToString())];

            var jsonString = System.IO.File.ReadAllText(fileName);

            game.SetGameStateFromJsonString(jsonString);
            
            if(callFromMenu)
            {
                return BattleShip(game); //***
            } 
            if(!callFromMenu) {
                BattleShipUI.GameUI.DrawBoard(game.GetBoard1(), game.GetBoard2(), game.PlayerA, game.PlayerB);
            }
            
            return "";
        }

        private static string LoadGameActionDatabase(BattleShipGame game, bool callFromMenu = false)
        {
            var dbOptions = new DbContextOptionsBuilder<AppDbContext>().UseSqlServer(
                @"
                    Server=SAILEKEYEV; 
                    Database=csharp;
                    Trusted_Connection=True;
                    MultipleActiveResultSets=true"
            ).Options;

            using var db = new AppDbContext(dbOptions);
            //using var db = new AppDbContext();
            var dbNames = db.Games
                .ToList();
            
            if(dbNames.Count == 0) {Console.WriteLine("There is not any save file in Cloud"); return "";}
            do
            {
                Console.WriteLine("Please choose your save file");
                for (var i = 0; i < db.Games.Count(); i++)
                {
                    Console.WriteLine($"{i} - {dbNames[i].CreatedDate} - {dbNames[i].Name}");
                }
                
                var fileNo = Console.ReadKey();
                if (int.Parse(fileNo.KeyChar.ToString()) <= db.Games.Count()-1 || int.Parse(fileNo.KeyChar.ToString()) < 0)
                {
                    //var fileName = dbNames[int.Parse(fileNo.KeyChar.ToString())].Name;
                    game.SetGameFromDatabase(dbNames[int.Parse(fileNo.KeyChar.ToString())].GameId);
                    break;
                }
                else
                {
                    Console.WriteLine("\nYou chose not existing save file. Please, try again");
                }
            } while (true);
            
            if(callFromMenu)
            {
                return BattleShip(game); //***
            } 
            if(!callFromMenu) {
                BattleShipUI.GameUI.DrawBoard(game.GetBoard1(), game.GetBoard2(), game.PlayerA, game.PlayerB);
            }
            
            return "";
        }
    }
}
