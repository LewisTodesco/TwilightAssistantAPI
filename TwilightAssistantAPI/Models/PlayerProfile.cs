using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TwilightAssistantAPI.Models
{
    public class PlayerProfile
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? GuidId { get; set; }

        public List<Game> Games { get; } = new();
    }
}
