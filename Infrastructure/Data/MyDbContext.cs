using Core.Entites;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class MyDbContext :DbContext
    {
        public MyDbContext(DbContextOptions options) :base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        // Dbset
        public DbSet<Product> products { get; set; }
    }
}
 