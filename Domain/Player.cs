using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain
{
    public class Player
    {
        public int PlayerId { get; set; }
        
        [MaxLength(64)]
        public string Name { get; set; } = null!;

        public EPlayerType EPlayerType { get; set; }

        public int? GameId { get; set; }
        public Game? Game { get; set; } = null!;

        public ICollection<Ship>? Ships { get; set; }
        
        public ICollection<BoardState>? BoardStates { get; set; }
    }
}