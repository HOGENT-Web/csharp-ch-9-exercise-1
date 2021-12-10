using Microsoft.AspNetCore.Components;
using Shared.Products.Categories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Client.Products.Components
{
    public partial class ProductFilters
    {
        private List<CategoryDto.Index> categories = new();
        [Parameter] public ProductFilter Filter { get; set; }
        [Inject] public ICategoryService CategoryService { get; set; }

        protected override Task OnInitializedAsync()
        {
            return GetCategories();
        }

        private async Task GetCategories()
        {
            var response = await CategoryService.GetIndexAsync(new());
            categories = response.Categories;
        }
    }
}
