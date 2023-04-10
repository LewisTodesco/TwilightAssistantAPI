global using Microsoft.EntityFrameworkCore;
using TwilightAssistantAPI.Models;

namespace TwilightAssistantAPI.Data


{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<PlayerProfile> PlayerProfiles { get; set; }
        
        public DbSet<Game> Games { get; set; }

        //Create a table with composite primary key. Both primary keys are foreign keys to the PlayerProfiles and Games tables Id columns.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PlayerProfile>()
                .HasMany(e => e.Games)
                .WithMany(e => e.PlayerProfiles)
                .UsingEntity<GamePlayer>(
                    l => l.HasOne<Game>().WithMany().HasForeignKey(e => e.GameId),
                    r => r.HasOne<PlayerProfile>().WithMany().HasForeignKey(e => e.PlayerProfileId));
        }

    }
}
