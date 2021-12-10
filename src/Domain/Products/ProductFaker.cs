using Domain.Common;

namespace Domain.Products
{
    public class ProductFaker : EntityFaker<Product>
    {
        public ProductFaker(bool hasRandomId = true) : base(hasRandomId)
        {
            var categories = new CategoryFaker(hasRandomId).Generate(5);
            CustomInstantiator(f => new Product(f.Commerce.ProductName(), f.Lorem.Paragraph(5), new Money(f.Random.Decimal(0, 200)), f.Random.Bool(), f.Image.PicsumUrl(), f.PickRandom(categories)));
        }
    }
}
