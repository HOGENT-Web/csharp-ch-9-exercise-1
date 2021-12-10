using Microsoft.EntityFrameworkCore;
using Persistence;
using Shared.Products.Categories;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Products
{
    public class CategoryService : ICategoryService
    {
        private readonly SportStoreDbContext dbContext;

        public CategoryService(SportStoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<CategoryResponse.GetIndex> GetIndexAsync(CategoryRequest.GetIndex request)
        {
            CategoryResponse.GetIndex response = new();

            response.Categories = await dbContext.Categories
            .Select(x => new CategoryDto.Index
            {
                Id = x.Id,
                Name = x.Name,
            }).ToListAsync();

            return response;
        }
    }
}
