using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;

namespace Domain.Orders
{
    public class DeliveryDate : ValueObject
    {
        public DateTime Date { get; }

        private DeliveryDate()
        {

        }

        public DeliveryDate(DateTime deliveryDate)
        {
            Date = Guard.Against.OutOfRange(deliveryDate.Date, nameof(deliveryDate), DateTime.Now.AddDays(1), DateTime.Now.AddMonths(1).Date);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Date;
        }
    }
}
