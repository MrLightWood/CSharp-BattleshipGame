using System.Collections.Generic;
using System.Text.Json.Serialization;
using Domain;
using Domain.Enums;

namespace BattleShipLogic
{
    public class GameState
    {
        public bool NextMoveByP1 { get; set; }
        public ECellState[][] Board1 { get; set; } = null!;
        public ECellState[][] Board2 { get; set; } = null!;
        public int Width { get; set; }
        public int Height { get; set; }
        public string PlayerA { get; set; } = null!;
        public string PlayerB { get; set; } = null!;
        public EBoatsCanTouch BoatsCanTouch { get; set; }
        public EPlayerType PlayerAType { get; set; }
        public EPlayerType PlayerBType { get; set; }
        public Dictionary<int, string> ShipsA { get; set; } = null!;
        public Dictionary<int, string> ShipsB { get; set; } = null!;
    }
}