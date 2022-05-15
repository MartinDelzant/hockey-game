using Microsoft.EntityFrameworkCore;

namespace HockeyGame.Models
{
    public class HockeyGameDbContext : DbContext
    {
        public HockeyGameDbContext(DbContextOptions<HockeyGameDbContext> options)
            : base(options)
        {}

        // Used for tests
        public HockeyGameDbContext(){}

        public virtual DbSet<Player> Player { get; set; }
        public virtual DbSet<Team> Team { get; set; }
        public virtual DbSet<PlayerTeam> PlayerTeams { get; set; }
    }
}
