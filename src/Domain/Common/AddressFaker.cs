using Bogus;

namespace Domain.Common
{
    public class AddressFaker : Faker<Address>
    {
        public AddressFaker()
        {
            var a = new Bogus.DataSets.Address();
            CustomInstantiator(f => new Address(a.Country(), a.City(), a.ZipCode(), a.StreetAddress()));
        }
    }
}
