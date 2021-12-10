using Microsoft.AspNetCore.Mvc;
using Shared.Products.Categories;
using System.Threading.Tasks;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet]
        public Task<CategoryResponse.GetIndex> GetIndexAsync([FromQuery] CategoryRequest.GetIndex request)
        {
            return categoryService.GetIndexAsync(request);
        }
    }
}
