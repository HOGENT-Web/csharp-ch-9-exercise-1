using Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class SportStoreDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        public SportStoreDbContext(DbContextOptions<SportStoreDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SportStoreDbContext).Assembly);
        }
    }
}
