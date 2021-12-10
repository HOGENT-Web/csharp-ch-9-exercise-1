using Append.Blazor.Sidepanel;
using Client.Products.Components;
using Microsoft.AspNetCore.Components;
using Shared.Products;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Client.Products
{
    public partial class Index : IDisposable
    {
        [Inject] public ISidepanelService Sidepanel { get; set; }
        [Inject] public IProductService ProductService { get; set; }
        private readonly ProductFilter filter = new();

        private List<ProductDto.Index> products;
        private int totalFilteredAmount;

        protected override async Task OnInitializedAsync()
        {
            filter.OnProductFilterChanged += FilterProductsAsync;
            ProductRequest.GetIndex request = new();
            var response = await ProductService.GetIndexAsync(request);
            products = response.Products;
            totalFilteredAmount = response.TotalAmount;
        }

        private void OpenCreateForm()
        {
            Sidepanel.Open<Create>("Product", "Toevoegen");
        }

        private async void FilterProductsAsync()
        {
            ProductRequest.GetIndex request = new()
            {
                MaximumPrice = filter.MaximumPrice,
                CategoryId = filter.CategoryId,
                MinimumPrice = filter.MinimumPrice,
                SearchTerm = filter.SearchTerm,
                Page = filter.Page,
                Amount = filter.Amount,
            };
            var response = await ProductService.GetIndexAsync(request);
            products = response.Products;
            totalFilteredAmount = response.TotalAmount;
            StateHasChanged();
        }

        public void Dispose()
        {
            filter.OnProductFilterChanged -= FilterProductsAsync;
        }
    }
}
