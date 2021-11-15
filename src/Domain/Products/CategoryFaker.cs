using Bogus;

namespace Domain.Products
{
    public class CategoryFaker : Faker<Category>
    {
        public CategoryFaker()
        {
            CustomInstantiator(f => new Category(f.Commerce.ProductMaterial()));
            RuleFor(x => x.Id, f => f.Random.Int());
        }
    }
}
