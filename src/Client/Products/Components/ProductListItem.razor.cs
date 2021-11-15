using Append.Blazor.Sidepanel;
using Client.Ordering;
using Microsoft.AspNetCore.Components;
using Shared.Products;
using System.Collections.Generic;

namespace Client.Products.Components
{
    public partial class ProductListItem
    {

        [Parameter] public ProductDto.Index Product { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] public ISidepanelService Sidepanel { get; set; }
        [Inject] public Cart Cart { get; set; }
        private void NavigateToDetail()
        {
            NavigationManager.NavigateTo($"product/{Product.Id}");
        }

        private void OpenEditForm()
        {
            var callback = EventCallback.Factory.Create(this, NavigateToDetail);

            var parameters = new Dictionary<string, object>
            {
                { nameof(Edit.ProductId), Product.Id },
                { nameof(Edit.OnProductChanged), callback  }
            };
            Sidepanel.Open<Edit>("Product", "Wijzigen", parameters);
        }

        private void AddToCart()
        {
            Cart.AddItem(Product.Id, Product.Name, Product.Price);
        }
    }


}
