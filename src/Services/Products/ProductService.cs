using Shared.Products;
using System.Linq;
using Persistence.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain.Products;
using System;

namespace Services.Products
{
    public class ProductService : IProductService
    {
        public ProductService(SportStoreDbContext dbContext)
        {
            _dbContext = dbContext;
            _products = dbContext.Products;
            _categories = dbContext.Categories;
        }

        private readonly SportStoreDbContext _dbContext;
        private readonly DbSet<Product> _products;
        private readonly DbSet<Category> _categories;

        private IQueryable<Product> GetProductById(int id) => _products
                .AsNoTracking()
                .Where(p => p.Id == id);

        public async Task<ProductResponse.Create> CreateAsync(ProductRequest.Create request)
        {
            ProductResponse.Create response = new();
            var category = await _categories.SingleOrDefaultAsync(c => c.Name == request.Product.Category);
            var product = _products.Add(new Product(
                request.Product.Name,
                request.Product.Description,
                request.Product.Price,
                request.Product.InStock,
                null,
                category
            ));
            await _dbContext.SaveChangesAsync();
            response.ProductId = product.Entity.Id;
            return response;
        }

        public async Task DeleteAsync(ProductRequest.Delete request)
        {
            _products.RemoveIf(p => p.Id == request.ProductId);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<ProductResponse.Edit> EditAsync(ProductRequest.Edit request)
        {
            ProductResponse.Edit response = new();
            var product = await GetProductById(request.ProductId).SingleOrDefaultAsync();

            if (product is not null)
            {
                var model = request.Product;
                var category = new Category(model.Category);

                // You could use a Product.Edit method here.
                product.Name = model.Name;
                product.Description = model.Description;
                product.InStock = model.InStock;
                product.Category = category;
                product.Price = model.Price;

                _dbContext.Entry(product).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
                response.ProductId = product.Id;
            }

            return response;
        }

        public async Task<ProductResponse.GetDetail> GetDetailAsync(ProductRequest.GetDetail request)
        {
            ProductResponse.GetDetail response = new();
            response.Product = await GetProductById(request.ProductId)
                .Select(x => new ProductDto.Detail
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price.Value,
                    IsEnabled = x.IsEnabled,
                    IsInStock = x.InStock,
                    Imagepath = x.ImageUrl,
                    CategoryName = x.Category.Name,
                })
                .SingleOrDefaultAsync();
            return response;
        }

        public async Task<ProductResponse.GetIndex> GetIndexAsync(ProductRequest.GetIndex request)
        {
            ProductResponse.GetIndex response = new();
            var query = _products.AsQueryable().AsNoTracking();

            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
                query = query.Where(x => x.Name.Contains(request.SearchTerm));

            if (!string.IsNullOrWhiteSpace(request.Category))
                query = query.Where(x => x.Category.Name.Equals(request.Category));

            if (request.MinimumPrice is not null)
                query = query.Where(x => x.Price.Value >= request.MinimumPrice);

            if (request.MaximumPrice is not null)
                query = query.Where(x => x.Price.Value <= request.MaximumPrice);

            if (request.OnlyActiveProducts)
                query = query.Where(x => x.IsEnabled);

            response.TotalAmount = query.Count();

            query = query.Take(request.Amount);
            query = query.Skip(request.Amount * request.Page);

            query.OrderBy(x => x.Name);
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
