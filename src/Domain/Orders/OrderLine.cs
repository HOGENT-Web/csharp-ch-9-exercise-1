using Ardalis.GuardClauses;
using Domain.Common;
using Domain.Products;

namespace Domain.Orders
{
    public class OrderLine : Entity
    {
        public Product Product { get; }
        public int Quantity { get; }
        public Money Price { get; }

        private OrderLine() { }
        public OrderLine(Product product, int quantity)
        {
            Product = Guard.Against.Null(product, nameof(product));
            Quantity = Guard.Against.Negative(quantity, nameof(quantity));
            Price = Product.Price;
        }
    }
}