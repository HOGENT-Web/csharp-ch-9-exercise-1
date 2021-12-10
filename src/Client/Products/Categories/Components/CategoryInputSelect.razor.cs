using Microsoft.AspNetCore.Components;
using Shared.Products.Categories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Client.Products.Categories.Components
{
    public partial class CategoryInputSelect
    {
        private int category;
        public int Category
        {
            get => category;
            private set
            {
                category = value;
                ValueChanged.InvokeAsync(value);
            }
        }
        private List<CategoryDto.Index> categories = new();

        [Parameter] public int Value { get; set; }
        [Parameter] public EventCallback<int> ValueChanged { get; set; }
        [Inject] public ICategoryService CategoryService { get; set; }
        protected override async Task OnInitializedAsync()
        {
            CategoryRequest.GetIndex request = new();
            var response = await CategoryService.GetIndexAsync(request);
            categories = response.Categories;
        }

        protected override void OnParametersSet()
        {
            category = Value;
        }
    }
}
