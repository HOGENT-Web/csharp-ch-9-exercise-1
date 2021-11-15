using System;
using System.Collections.Generic;

namespace Shared.Products
{
    public static class ProductResponse
    {
        public class GetIndex
        {
            public List<ProductDto.Index> Products { get; set; } = new();
            public int TotalAmount { get; set; }
        }

        public class GetDetail
        {
            public ProductDto.Detail Product { get; set; }
        }

        public class Delete
        {
        }

        public class Create
        {
            public int ProductId { get; set; }
            public Uri UploadUri { get; set; }
        }

        public class Edit
        {
            public int ProductId { get; set; }
        }
    }
}
