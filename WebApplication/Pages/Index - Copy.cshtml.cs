using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using WebApplication.Data;
using BattleShipLogic;
using Domain;

namespace WebApplication.Pages
{
    public class IndexModels : PageModel
    {
        [BindProperty]
        //public BattleShipGame Game { get; set; } = new BattleShipGame();
        public BattleShipGame Game { get; set; } = null!;

        [MaxLength(20)]
        [BindProperty] 
        public string PlayerA { get; set; } = null!;
        
        [BindProperty] 
        public string PlayerB { get; set; } = null!;

        [BindProperty] 
        public string GameStarted { get; set; } = null!;
        
        [BindProperty(SupportsGet = true)]
        public int X { get; set; }
        [BindProperty(SupportsGet = true)]
        public int Y { get; set; }
        public int[,] GameBoard { get; set; } = new int[10, 10];
        
        private readonly Random _rnd = new Random();
        
        public void OnGet()
        {
            /*
            var dbOptions = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer(
                @"
                    Server=SAILEKEYEV; 
                    Database=csharp;
                    Trusted_Connection=True;
                    MultipleActiveResultSets=true"
            ).Options;

            using var db = new ApplicationDbContext(dbOptions);
            var dbGame = db.Games.Single(x=>x.Name == "okko");
            var dbPlayerA = db.Players
                .Single(x => x.PlayerId == dbGame.PlayerAId);
            */
            if(GameStarted != "true")
            {
                GameStarted = "false";
                Game = new BattleShipGame();
                //Game.BattleShipGameInit("PlayerA!", "PlayerB!", 10, 10, 0);
            }
            else
            {
                Game.MakeAMove(X, Y);
            }
        }
        /*
        public void OnPost()
        {
            if(!ModelState.IsValid)
            {
                ModelState.AddModelError("Name", "We have a problem");
                ModelState.AddModelError("XXXX", "We have XXXX problem");
            }
            
            for (int y = 0; y < GameBoard.GetLength(1); y++)
            {
                for (int x = 0; x < GameBoard.GetLength(0); x++)
                {
                    GameBoard[x, y] = _rnd.Next(0, 100);
                }    
            }
        }
        */
        public IActionResult OnPost()
        {
            if(ModelState.IsValid)
            {
                //Game.BattleShipGameInit(PlayerA!, PlayerB!, 10, 10, 0);
                Game.GenerateBoard();
                //GameStarted = "true";
                return Page();
            }
            //Game.BattleShipGameInit(PlayerA!, PlayerB!, 10, 10, 0);
            Game.GenerateBoard();
            //GameStarted = "true";
            return Page();
        }
    }
}