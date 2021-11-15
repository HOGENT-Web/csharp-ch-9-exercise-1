using Bogus;
using Domain.Common;

namespace Domain.Customers
{
    public class CustomerFaker : Faker<Customer>
    {
        public CustomerFaker()
        {
            CustomInstantiator(f => new Customer(new CustomerName(f.Person.FirstName, f.Person.LastName), new AddressFaker()));
            RuleFor(x => x.Id, f => f.Random.Int());
        }
    }
}
