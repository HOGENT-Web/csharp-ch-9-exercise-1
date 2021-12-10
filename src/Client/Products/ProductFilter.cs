using System;

namespace Client.Products
{
    public class ProductFilter
    {
        public event Action OnProductFilterChanged;
        private int page;
        private int amount = 25;
        private string searchTerm;
        private int? categoryId;
        private decimal? minimumPrice;
        private decimal? maximumPrice;

        private void NotifyStateChanged() => OnProductFilterChanged.Invoke();

        public string SearchTerm
        {
            get => searchTerm;
            set
            {
                searchTerm = value;
                NotifyStateChanged();
            }
        }

        public int? CategoryId
        {
            get => categoryId;
            set
            {
                categoryId = value;
                NotifyStateChanged();
            }
        }

        public decimal? MinimumPrice
        {
            get => minimumPrice;
            set
            {
                minimumPrice = value;
                NotifyStateChanged();
            }
        }

        public decimal? MaximumPrice
        {
            get => maximumPrice;
            set
            {
                maximumPrice = value;
                NotifyStateChanged();
            }
        }

        public int Amount
        {
            get => amount;
            set
            {
                amount = value;
                NotifyStateChanged();
            }
        }

        public int Page
        {
            get => page;
            set
            {
                page = value;
                NotifyStateChanged();
            }
        }

    }
}
