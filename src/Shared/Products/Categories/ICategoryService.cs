using System.Threading.Tasks;

namespace Shared.Products.Categories
{
    public interface ICategoryService
    {
        Task<CategoryResponse.GetIndex> GetIndexAsync(CategoryRequest.GetIndex request);
    }
}
