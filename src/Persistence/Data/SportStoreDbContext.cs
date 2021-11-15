using Persistence.Data.Configuration;
using Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Data
{
    public class SportStoreDbContext : DbContext
    {
        public SportStoreDbContext(DbContextOptions<SportStoreDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new CategoryEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ProductEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new OrderEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new OrderLineEntityTypeConfiguration());
        }
    }
}
