using Append.Blazor.Sidepanel;
using Microsoft.AspNetCore.Components;

namespace Client.Ordering.Components
{
    public partial class ShoppingCart
    {
        [Inject] public ISidepanelService Sidepanel { get; set; }
        [Inject] public Cart Cart { get; set; }
    }
}
