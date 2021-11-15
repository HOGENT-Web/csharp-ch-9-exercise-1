using Microsoft.AspNetCore.Mvc;
using Shared.Products;
using System.Threading.Tasks;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }


        [HttpGet]
        public Task<ProductResponse.GetIndex> GetIndexAsync([FromQuery] ProductRequest.GetIndex request)
        {
            return productService.GetIndexAsync(request);
        }

        [HttpGet("{ProductId}")]
        public Task<ProductResponse.GetDetail> GetDetailAsync([FromRoute] ProductRequest.GetDetail request)
        {
            return productService.GetDetailAsync(request);
        }

        [HttpDelete("{ProductId}")]
        public Task DeleteAsync([FromRoute] ProductRequest.Delete request)
        {
            return productService.DeleteAsync(request);
        }

        [HttpPost]
        public Task<ProductResponse.Create> CreateAsync([FromBody] ProductRequest.Create request)
        {
            return productService.CreateAsync(request);
        }

        [HttpPut]
        public Task<ProductResponse.Edit> EditAsync([FromBody] ProductRequest.Edit request)
        {
            return productService.EditAsync(request);
        }
    }
}
