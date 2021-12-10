using System;
using System.Collections.Generic;
using System.Linq;

namespace Client.Ordering
{
    public class Cart
    {
        private List<Item> items = new();
        public IReadOnlyList<Item> Items => items.AsReadOnly();
        public event Action OnCartChanged;
        public void NotifyCartChanged() => OnCartChanged?.Invoke();
        public decimal Total => items.Sum(x => x.Total);
        public void AddItem(int productId, string name, decimal price)
        {
            var existingItem = items.SingleOrDefault(x => x.ProductId == productId);

            if (existingItem == null)
            {
                Item item = new Item(productId, name, price, 1);
                items.Add(item);
            }
            else
            {
                existingItem.Amount++;
            }
            NotifyCartChanged();
        }

        public void RemoveItem(Item item)
        {
            items.Remove(item);
            NotifyCartChanged();
        }

        public class Item
        {
            public int ProductId { get; init; }
            public string Name { get; init; }
            public int Amount { get; set; }
            public decimal Price { get; init; }
            public decimal Total => Price * Amount;

            public Item(int productId, string name, decimal price, int amount)
            {
                ProductId = productId;
                Name = name;
                Price = price;
                Amount = amount;
            }
        }
    }
}
