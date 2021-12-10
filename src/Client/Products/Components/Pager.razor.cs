using Microsoft.AspNetCore.Components;

namespace Client.Products.Components
{
    public partial class Pager
    {
        private bool hasNoMorePrevious => Filter.Page == 0;
        private bool hasNoMoreNext => (Filter.Page + 1) * Filter.Amount >= TotalAmount;
        [Parameter] public ProductFilter Filter { get; set; }
        [Parameter] public int TotalAmount { get; set; }

        private void GoToPrevious()
        {
            if (hasNoMorePrevious)
                return;

            Filter.Page--;
        }

        private void GoToNext()
        {
            if (hasNoMoreNext)
                return;

            Filter.Page++;

        }
    }
}
