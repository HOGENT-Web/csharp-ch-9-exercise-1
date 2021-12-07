using Ardalis.GuardClauses;
using Domain.Common;
using Domain.Orders;
using Domain.Products;
using System;
using System.Collections.Generic;

namespace Domain.Customers
{
    public class Customer : Entity
    {
        private readonly List<Subscription> _subscriptions = new();


        public CustomerName Name { get; private set; }
        public Address Address { get; private set; }
        public List<Order> Orders { get; set; }
        public IReadOnlyList<Subscription> Subscriptions => _subscriptions.AsReadOnly();

        /// <summary>
        /// Entity Framework Constructor
        /// </summary>
        private Customer() { }

        public Customer(CustomerName name, Address address)
        {
            Name = Guard.Against.Null(name, nameof(name));
            Address = Guard.Against.Null(address, nameof(address));
        }

        public Order PlaceOrder(Cart cart, DeliveryDate deliveryDate, bool hasGiftWrapping, Address shippingAddress)
        {
            var order = new Order(cart, deliveryDate, hasGiftWrapping, this, shippingAddress);
            Orders.Add(order);
            return order;
        }

        public void Subscribe(Subscription subscription)
        {
            if (_subscriptions.Contains(subscription))
                throw new ArgumentException($"{nameof(Customer)}:{Name} is already subscribed to {subscription.Name}");

            _subscriptions.Add(subscription);
        }

        public void Unsubscribe(Subscription subscription)
        {
            if (!_subscriptions.Contains(subscription))
                throw new ArgumentException($"{nameof(Customer)}:{Name} was never subscribed to {subscription.Name}");

            _subscriptions.Remove(subscription);
        }
    }
}
