using Append.Blazor.Sidepanel;
using Microsoft.AspNetCore.Components;
using Shared.Products;
using System.Threading.Tasks;

namespace Client.Products.Components
{
    public partial class Edit
    {
        [Parameter] public int ProductId { get; set; }
        [Parameter] public EventCallback OnProductChanged { get; set; }
        private ProductDto.Mutate model = new();
        [Inject] public IProductService ProductService { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] public ISidepanelService Sidepanel { get; set; }

        protected override async Task OnInitializedAsync()
        {
            ProductDto.Detail product;
            // Fetch the latest version of the product before editing.
            var response = await ProductService.GetDetailAsync(new ProductRequest.GetDetail { ProductId = ProductId });
            product = response.Product;

            model = new ProductDto.Mutate
            {
                CategoryId = product.Category.Id,
                Description = product.Description,
                InStock = product.IsInStock,
                Name = product.Name,
                Price = product.Price,
            };
        }

        private async Task EditProductAsync()
        {
            ProductRequest.Edit request = new()
            {
                ProductId = ProductId,
                Product = model
            };
            await ProductService.EditAsync(request);
            await OnProductChanged.InvokeAsync();
            Sidepanel.Close();
        }
    }
}
