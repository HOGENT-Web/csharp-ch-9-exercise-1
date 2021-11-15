using Domain.Common;
using Domain.Products;
using Services.Common;
using Shared.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Products
{
    public class FakeProductService : IProductService
    {
        private static readonly List<Product> products = new();
        private readonly IStorageService storageService;

        static FakeProductService()
        {
            var productFaker = new ProductFaker();
            products = productFaker.Generate(50);
        }

        public FakeProductService(IStorageService storageService)
        {
            this.storageService = storageService;
        }

        public async Task<ProductResponse.GetDetail> GetDetailAsync(ProductRequest.GetDetail request)
        {
            await Task.Delay(100);
            ProductResponse.GetDetail response = new();
            response.Product = products.Select(x => new ProductDto.Detail
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price.Value,
                IsEnabled = x.IsEnabled,
                IsInStock = x.InStock,
                Imagepath = x.ImageUrl,
                CategoryName = x.Category.Name,
            }).SingleOrDefault(x => x.Id == request.ProductId);
            return response;
        }

        public async Task<ProductResponse.GetIndex> GetIndexAsync(ProductRequest.GetIndex request)
        {
            await Task.Delay(100);
            ProductResponse.GetIndex response = new();
            var query = products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
                query = query.Where(x => x.Name.Contains(request.SearchTerm,StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(request.Category))
                query = query.Where(x => x.Category.Name.Equals(request.Category,StringComparison.OrdinalIgnoreCase));

            if (request.MinimumPrice is not null)
                query = query.Where(x => x.Price.Value >= request.MinimumPrice);

            if (request.MaximumPrice is not null)
                query = query.Where(x => x.Price.Value <= request.MaximumPrice);

            if (request.OnlyActiveProducts)
                query = query.Where(x => x.IsEnabled);

            response.TotalAmount = query.Count();

            query = query.Skip(request.Amount * request.Page);
            query = query.Take(request.Amount);

            query.OrderBy(x => x.Name);
            response.Products = query.Select(x => new ProductDto.Index
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price.Value,
                Imagepath = x.ImageUrl,
            }).ToList();
            return response;
        }

        public async Task DeleteAsync(ProductRequest.Delete request)
        {
            await Task.Delay(100);
            var p = products.SingleOrDefault(x => x.Id == request.ProductId);
            products.Remove(p);
        }

        public async Task<ProductResponse.Create> CreateAsync(ProductRequest.Create request)
        {
            await Task.Delay(100);
            ProductResponse.Create response = new();

            var model = request.Product;
            var price = new Money(model.Price);
            var category = new Category(model.Category);
            var imageFilename = Guid.NewGuid().ToString();
            var imagePath = $"{storageService.StorageBaseUri}{imageFilename}";
            var product = new Product(model.Name, model.Description, price, model.InStock, imagePath, category)
            {
                Id = products.Max(x => x.Id) + 1
            };

            products.Add(product);
            var uploadUri = storageService.CreateUploadUri(imageFilename);
            response.ProductId = product.Id;
            response.UploadUri = uploadUri;

            return response;
        }

        public async Task<ProductResponse.Edit> EditAsync(ProductRequest.Edit request)
        {
            await Task.Delay(100);
            ProductResponse.Edit response = new();
            var product = products.SingleOrDefault(x => x.Id == request.ProductId);
            
            var model = request.Product;
            var price = new Money(model.Price);
            var category = new Category(model.Category);

            // You could use a Product.Edit method here.
            product.Name = model.Name;
            product.Description = model.Description;
            product.InStock = model.InStock;
            product.Category = category;
            product.Price = price;

            response.ProductId = product.Id;
            return response;
        }
    }

}
