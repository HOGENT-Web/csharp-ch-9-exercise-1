using Bogus;
using Domain.Common;

namespace Domain.Products
{
    public class ProductFaker : Faker<Product>
    {
        public ProductFaker()
        {
            CustomInstantiator(f => new Product(f.Commerce.ProductName(), f.Lorem.Paragraph(5), new Money(f.Random.Decimal(0, 200)), f.Random.Bool(),f.Image.PicsumUrl(), new CategoryFaker()));
            RuleFor(x => x.Id, f => f.Random.Int(1));
            UseSeed(1337);
        }
    }
}
