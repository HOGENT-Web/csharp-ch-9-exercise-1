using Domain.Common;
using Domain.Products;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Orders
{
    public class Cart
    {
        private readonly List<CartLine> _lines = new();

        public IReadOnlyList<CartLine> Lines => _lines.AsReadOnly();
        public Money Total => _lines.Sum(x => x.Total);

        public CartLine AddItem(Product product, int quantity)
        {
            var existingLine = _lines.SingleOrDefault(x => x.Product.Equals(product));
            if (existingLine is not null)
            {
                existingLine.IncreaseQuantity(quantity);
                return existingLine;
            }
            else
            {
                CartLine line = new(product, quantity);
                _lines.Add(line);
                return line;
            }
        }

        public void RemoveLine(Product product)
        {
            CartLine line = _lines.SingleOrDefault(l => l.Product.Equals(product));
            if (line != null)
                _lines.Remove(line);
        }

        public void Clear()
        {
            _lines.Clear();
        }

    }
}
