namespace TwilightAssistantAPI.Models
{
    public class Game
    {
        public int Id { get; set; }
        public DateTime GameDate { get; set; }
        public bool IsActive { get; set; }
        public string? GuidId { get; set; }

        public List<PlayerProfile> PlayerProfiles { get; } = new();
    }
}
