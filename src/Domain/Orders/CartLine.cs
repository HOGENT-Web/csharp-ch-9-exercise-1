using Ardalis.GuardClauses;
using Domain.Common;
using Domain.Products;

namespace Domain.Orders
{
    public class CartLine
    {
        public Product Product { get; }
        public int Quantity { get; private set; }
        public Money Total => Product.Price * Quantity;

        public CartLine(Product product, int quantity)
        {
            Product = Guard.Against.Null(product, nameof(product));
            Quantity = Guard.Against.Negative(quantity, nameof(quantity));
        }

        public void IncreaseQuantity(int quantity)
        {
            Quantity += quantity;
        }
    }
}
