using Domain.Common;

namespace Domain.Products
{
    public class CategoryFaker : EntityFaker<Category>
    {
        public CategoryFaker(bool hasRandomId = true) : base(hasRandomId)
        {
            CustomInstantiator(f => new Category(f.Commerce.ProductMaterial()));
        }
    }
}
