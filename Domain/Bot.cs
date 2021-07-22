namespace Domain
{
    public class Bot
    {
        public int BotId { get; set; }

        public int? BotFirstMoveX { get; set; }
        public int? BotFirstMoveY { get; set; }

        public int? BotLastShipHitX { get; set; }
        public int? BotLastShipHitY { get; set; }

        public string? BotCurrentDirection { get; set; }

        public int PlayerId { get; set; }
        //public Player Player { get; set; } = null!;
    }
}