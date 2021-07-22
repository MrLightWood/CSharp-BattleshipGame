using Domain.Enums;

namespace Domain
{
    public class BoardState
    {
        public int BoardStateId { get; set; }
        public int PlayerId { get; set; }
        public Player Player { get; set; } = null!;
        public int ArrayIndexX { get; set; }
        public int ArrayIndexY { get; set; }
        public ECellState Value { get; set; }
    }
}