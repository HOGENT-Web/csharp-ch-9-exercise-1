using Domain.Products;

namespace Persistence.Data
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
            if (_dbContext.Database.EnsureCreated())
            {
                SeedProducts();
            }
        }

        private void SeedProducts()
        {
            var products = new ProductFaker()
                .RuleFor(p => p.Id, () => 0) // Remove the id, database column is auto generated
                .RuleFor(p => p.Category, new CategoryFaker().RuleFor(c => c.Id, 0))
                .Generate(100);
            _dbContext.Products.AddRange(products);
            _dbContext.SaveChanges();
        }
    }
}
