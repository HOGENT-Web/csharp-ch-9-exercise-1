using Client.Infrastructure;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Shared.Products;
using System.Threading.Tasks;

namespace Client.Products.Components
{
    public partial class Create
    {
        private ProductDto.Mutate product = new();
        private IBrowserFile image;
        [Inject] public IProductService ProductService { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] public StorageService StorageService { get; set; }
        private async Task CreateProductAsync()
        {
            ProductRequest.Create request = new()
            {
                Product = product
            };

            var response = await ProductService.CreateAsync(request);

            await StorageService.UploadImageAsync(response.UploadUri, image);
            NavigationManager.NavigateTo($"product/{response.ProductId}");
        }

        private void LoadImage(InputFileChangeEventArgs args)
        {
            image = args.File;
            product.ImageAmount = 1;
        }
    }
}
