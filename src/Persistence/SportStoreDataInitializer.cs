using Domain.Products;

namespace Persistence
{
    public class SportStoreDataInitializer
    {
        private readonly SportStoreDbContext _dbContext;

        public SportStoreDataInitializer(SportStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void SeedData()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Database.EnsureCreated();
            SeedProducts();
        }

        private void SeedProducts()
        {
            var products = new ProductFaker(hasRandomId: false).Generate(100);
            _dbContext.Products.AddRange(products);
            _dbContext.SaveChanges();
        }
    }
}
