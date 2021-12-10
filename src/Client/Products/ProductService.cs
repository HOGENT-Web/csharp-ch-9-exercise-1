using Client.Extensions;
using Shared.Products;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Client.Products
{
    public class ProductService : IProductService
    {
        private readonly HttpClient client;
        private const string endpoint = "api/product";
        public ProductService(HttpClient client)
        {
            this.client = client;
        }
        public async Task<ProductResponse.Create> CreateAsync(ProductRequest.Create request)
        {
            var response = await client.PostAsJsonAsync(endpoint, request);
            return await response.Content.ReadFromJsonAsync<ProductResponse.Create>();
        }

        public async Task DeleteAsync(ProductRequest.Delete request)
        {
            await client.DeleteAsync($"{endpoint}/{request.ProductId}");
        }

        public async Task<ProductResponse.GetDetail> GetDetailAsync(ProductRequest.GetDetail request)
        {
            var response = await client.GetFromJsonAsync<ProductResponse.GetDetail>($"{endpoint}/{request.ProductId}");
            return response;
        }

        public async Task<ProductResponse.GetIndex> GetIndexAsync(ProductRequest.GetIndex request)
        {
            var queryParameters = request.GetQueryString();
            var response = await client.GetFromJsonAsync<ProductResponse.GetIndex>($"{endpoint}?{queryParameters}");
            return response;
        }

        public async Task<ProductResponse.Edit> EditAsync(ProductRequest.Edit request)
        {
            var response = await client.PutAsJsonAsync(endpoint, request);
            return await response.Content.ReadFromJsonAsync<ProductResponse.Edit>();
        }
    }
}
