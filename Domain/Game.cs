using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Enums;

namespace Domain
{
    public class Game
    {
        public int GameId { get; set; }

        public string CreatedDate { get; set; } = DateTime.Now.ToLongDateString();

        [MaxLength(64)]
        public string Name { get; set; } = null!;
        
        public int BoardWidth { get; set; }
        public int BoardHeight { get; set; }

        // enums are actually int in database
        public EBoatsCanTouch EBoatsCanTouch { get; set; }

        public ENextMoveAfterHit ENextMoveAfterHit { get; set; }
        
        public int PlayerAId { get; set; }
        public Player PlayerA { get; set; } = null!;
        
        public int PlayerBId { get; set; }
        public Player PlayerB { get; set; } = null!;

        //public ICollection<Player> Players { get; set; } = null!;
        //public int BoardStateId { get; set; }
        //public BoardState BoardState { get; set; } = null!;
    }
}