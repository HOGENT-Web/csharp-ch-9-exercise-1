using Domain.Common;
using Domain.Customers;
using System.Collections.Generic;

namespace Domain.Products
{
    public class Subscription : Product
    {
        private readonly List<Customer> _customers = new();

        public IReadOnlyList<Customer> Customers => _customers.AsReadOnly();
        /// <summary>
        /// Entity Framework Constructor
        /// </summary>
        private Subscription() { }
        public Subscription(string name, string description, Money price, string imageUrl, Category category)
                      : base(name, description, price, true, imageUrl, category)
        {

        }
    }
}
