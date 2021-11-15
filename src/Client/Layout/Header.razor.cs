using Append.Blazor.Sidepanel;
using Client.Ordering;
using Client.Ordering.Components;
using Microsoft.AspNetCore.Components;
using System;

namespace Client.Layout
{
    public partial class Header : IDisposable
    {
        private bool isOpen;
        private string isOpenClass => isOpen ? "is-active" : null;

        [Inject] public ISidepanelService Sidepanel { get; set; }
        [Inject] public Cart Cart { get; set; }

        protected override void OnInitialized()
        {
            Cart.OnCartChanged += StateHasChanged;
        }

        public void Dispose()
        {
            Cart.OnCartChanged -= StateHasChanged;
        }

        private void ToggleMenuDisplay()
        {
            isOpen = !isOpen;
        }

        private void OpenShoppingCart()
        {
            Sidepanel.Open<ShoppingCart>("Winkelwagen");
        }


    }
}
