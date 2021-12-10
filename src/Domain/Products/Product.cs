using Ardalis.GuardClauses;
using Domain.Common;

namespace Domain.Products
{
    public class Product : Entity
    {
        private string name;
        private Category category;
        private Money price;
        public string Name
        {
            get { return name; }
            set { name = Guard.Against.NullOrWhiteSpace(value, nameof(name)); }
        }

        public Category Category
        {
            get => category;
            set => category = Guard.Against.Null(value, nameof(category));
        }

        public string Description { get; set; }
        public Money Price
        {
            get => price;
            set => price = Guard.Against.Null(value, nameof(price));
        }
        public bool InStock { get; set; }
        public string ImageUrl { get; set; }

        private Product() { }
        public Product(string name, string description, Money price, bool inStock, string imageUrl, Category category)
        {
            Name = name;
            Category = category;
            Description = description;
            Price = Guard.Against.Null(price, nameof(price));
            InStock = inStock;
            ImageUrl = imageUrl;
        }
    }
}
