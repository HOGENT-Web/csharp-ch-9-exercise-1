using Microsoft.AspNetCore.Components;

namespace Client.Products.Components
{
    public partial class ProductFilters
    {
        [Parameter] public ProductFilter Filter { get; set; }
    }
}
