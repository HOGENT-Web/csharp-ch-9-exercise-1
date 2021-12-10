using Shared.Products.Categories;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Client.Products.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient client;
        private const string endpoint = "api/category";
        public CategoryService(HttpClient client)
        {
            this.client = client;
        }
        public async Task<CategoryResponse.GetIndex> GetIndexAsync(CategoryRequest.GetIndex request)
        {
            // This could be cached if needed 
            var response = await client.GetFromJsonAsync<CategoryResponse.GetIndex>($"{endpoint}");
            return response;
        }
    }
}
