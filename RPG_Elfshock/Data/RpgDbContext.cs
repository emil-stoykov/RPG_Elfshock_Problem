using Microsoft.EntityFrameworkCore;
using Models;

namespace Data
{
    public class RpgDbContext : DbContext
    {
        public RpgDbContext(DbContextOptions<RpgDbContext> options) : base(options) {}
        public DbSet<Character> Characters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Character>()
                .Property(c => c.CharacterCreationDate)
                .HasDefaultValue(DateTime.Now);
        }
    }
}
