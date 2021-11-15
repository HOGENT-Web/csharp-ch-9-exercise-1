using Ardalis.GuardClauses;
using Domain.Common;
using Domain.Orders;
using System.Collections.Generic;

namespace Domain.Customers
{
    public class Customer : Entity
    {
        public CustomerName Name { get; private set; }
        public Address Address { get; private set; }
        private readonly List<Order> _orders = new();
        public IReadOnlyList<Order> Orders => _orders.AsReadOnly();

        public Customer(CustomerName name, Address address)
        {
            Name = Guard.Against.Null(name, nameof(name));
            Address = Guard.Against.Null(address, nameof(address));
        }

        public Order PlaceOrder(Cart cart, DeliveryDate deliveryDate, bool hasGiftWrapping, Address shippingAddress)
        {
            var order = new Order(cart, deliveryDate, hasGiftWrapping, this, shippingAddress);
            _orders.Add(order);
            return order;
        }
    }
}
