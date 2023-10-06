using ExoASP.Models.Congifs;
using ExoASP.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExoASP.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Game> games { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfig());
            modelBuilder.ApplyConfiguration(new GameConfig());
        }


    }
}
