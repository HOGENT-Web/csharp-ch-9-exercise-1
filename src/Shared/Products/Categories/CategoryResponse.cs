using System.Collections.Generic;

namespace Shared.Products.Categories
{
    public static class CategoryResponse
    {
        public class GetIndex
        {
            public List<CategoryDto.Index> Categories { get; set; } = new();
        }
    }
}
