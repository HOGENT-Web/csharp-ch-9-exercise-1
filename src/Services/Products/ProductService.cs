using Domain.Products;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Services.Common;
using Shared.Products;
using Shared.Products.Categories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Products
{
    public class ProductService : IProductService
    {
        private readonly SportStoreDbContext dbContext;
        private readonly IStorageService storageService;

        public ProductService(SportStoreDbContext dbContext, IStorageService storageService)
        {
            this.dbContext = dbContext;
            this.storageService = storageService;
        }


        public async Task<ProductResponse.Create> CreateAsync(ProductRequest.Create request)
        {
            ProductResponse.Create response = new();

            var category = await dbContext.Categories.SingleAsync(x => x.Id == request.Product.CategoryId);
            var imageFilename = Guid.NewGuid().ToString();
            var imagePath = $"{storageService.StorageBaseUri}{imageFilename}";

            var product = new Product(request.Product.Name, request.Product.Description, request.Product.Price, request.Product.InStock, imagePath, category);

            dbContext.Products.Add(product);
            await dbContext.SaveChangesAsync();

            response.ProductId = product.Id;
            response.UploadUri = storageService.CreateUploadUri(imageFilename);

            return response;
        }

        public async Task DeleteAsync(ProductRequest.Delete request)
        {
            dbContext.Products.RemoveIf(p => p.Id == request.ProductId);
            await dbContext.SaveChangesAsync();
        }

        public async Task<ProductResponse.Edit> EditAsync(ProductRequest.Edit request)
        {
            ProductResponse.Edit response = new();

            var product = await dbContext.Products
                                          .Include(x => x.Category)
                                          .SingleAsync(x => x.Id == request.ProductId);

            var category = await dbContext.Categories.SingleAsync(x => x.Id == request.Product.CategoryId);

            var model = request.Product;
            // You could use a Product.Edit method here.
            product.Name = model.Name;
            product.Description = model.Description;
            product.InStock = model.InStock;
            product.Category = category;
            product.Price = model.Price;
            product.Category = category;

            await dbContext.SaveChangesAsync();
            response.ProductId = product.Id;

            return response;
        }

        public async Task<ProductResponse.GetDetail> GetDetailAsync(ProductRequest.GetDetail request)
        {
            ProductResponse.GetDetail response = new();
            response.Product = await dbContext.Products
                    //.AsNoTracking() // Is not needed since AsNoTracking is always used when using .Select()
                    .Select(x => new ProductDto.Detail
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Description = x.Description,
                        Price = x.Price.Value,
                        IsEnabled = x.IsEnabled,
                        IsInStock = x.InStock,
                        Imagepath = x.ImageUrl,
                        Category = new CategoryDto.Index
                        {
                            Id = x.Category.Id,
                            Name = x.Category.Name
                        },
                    })
                    .SingleAsync(x => x.Id == request.ProductId);

            return response;
        }

        public async Task<ProductResponse.GetIndex> GetIndexAsync(ProductRequest.GetIndex request)
        {
            ProductResponse.GetIndex response = new();
            var query = dbContext.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
                query = query.Where(x => x.Name.Contains(request.SearchTerm));

            if (request.CategoryId.HasValue)
                query = query.Where(x => x.Category.Id.Equals(request.CategoryId));

            if (request.MinimumPrice.HasValue)
                query = query.Where(x => x.Price.Value >= request.MinimumPrice);

            if (request.MaximumPrice.HasValue)
                query = query.Where(x => x.Price.Value <= request.MaximumPrice);

            if (request.OnlyActiveProducts)
                query = query.Where(x => x.IsEnabled);

            response.TotalAmount = query.Count();

            query = query.OrderBy(x => x.Name);
            query = query.Skip(request.Amount * request.Page);
            query = query.Take(request.Amount);

            response.Products = await query.Select(x => new ProductDto.Index
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price.Value,
                Imagepath = x.ImageUrl,
            }).ToListAsync();
            return response;
        }
    }
}
