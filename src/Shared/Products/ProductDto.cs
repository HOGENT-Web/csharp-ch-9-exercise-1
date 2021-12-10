using FluentValidation;
using Shared.Products.Categories;

namespace Shared.Products
{
    public static class ProductDto
    {
        public class Index
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Price { get; set; }
            public string Imagepath { get; set; }
        }

        public class Detail : Index
        {
            public bool IsEnabled { get; set; }
            public bool IsInStock { get; set; }
            public CategoryDto.Index Category { get; set; }
        }

        public class Mutate
        {
            public string Name { get; set; }
            public decimal Price { get; set; }
            public string Description { get; set; }
            public int CategoryId { get; set; }
            public bool InStock { get; set; }
            public int ImageAmount { get; set; }

            public class Validator : AbstractValidator<Mutate>
            {
                public Validator()
                {
                    RuleFor(x => x.Name).NotEmpty().Length(1, 250);
                    RuleFor(x => x.Price).InclusiveBetween(1, 250);
                    RuleFor(x => x.CategoryId).NotEmpty();
                    //RuleFor(x => x.ImageAmount).GreaterThanOrEqualTo(1);
                }
            }
        }
    }
}
