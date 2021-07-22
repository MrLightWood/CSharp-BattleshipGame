using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Ship
    {
        public int ShipId { get; set; }

        [Range(1, 5)]
        public int Size { get; set; }
        [MaxLength(5)]
        public string Cells { get; set; } = null!;

        public int? PlayerId { get; set; }
        public Player Player { get; set; } = null!;
    }
}