using System.Threading.Tasks;

namespace Shared.Products
{
    public interface IProductService
    {
        Task<ProductResponse.GetIndex> GetIndexAsync(ProductRequest.GetIndex request);
        Task<ProductResponse.GetDetail> GetDetailAsync(ProductRequest.GetDetail request);
        Task DeleteAsync(ProductRequest.Delete request);
        Task<ProductResponse.Create> CreateAsync(ProductRequest.Create request);
        Task<ProductResponse.Edit> EditAsync(ProductRequest.Edit request);
    }
}
