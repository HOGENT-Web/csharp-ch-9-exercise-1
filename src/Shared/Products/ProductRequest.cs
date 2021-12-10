namespace Shared.Products
{
    public static class ProductRequest
    {
        public class GetIndex
        {
            public string SearchTerm { get; set; }
            public int? CategoryId { get; set; }
            public bool OnlyActiveProducts { get; set; }
            public decimal? MinimumPrice { get; set; }
            public decimal? MaximumPrice { get; set; }
            public int Page { get; set; }
            public int Amount { get; set; } = 25;
        }

        public class GetDetail
        {
            public int ProductId { get; set; }
        }

        public class Delete
        {
            public int ProductId { get; set; }
        }

        public class Create
        {
            public ProductDto.Mutate Product { get; set; }
        }

        public class Edit
        {
            public int ProductId { get; set; }
            public ProductDto.Mutate Product { get; set; }
        }
    }
}
