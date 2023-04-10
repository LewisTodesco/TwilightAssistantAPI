namespace TwilightAssistantAPI.Models
{
    public class GamePlayer
    {
        public int GameId { get; set; }
        public int PlayerProfileId { get; set; }
        public string? Race { get; set; }
        public string? RaceLogo { get; set; }
        public string? ElapsedTime { get; set; }
        public string? StrategyCard { get; set; }
        public bool? IsWin { get; set; }

    }
}
