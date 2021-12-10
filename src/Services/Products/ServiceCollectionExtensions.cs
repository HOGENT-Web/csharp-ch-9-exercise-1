using Microsoft.Extensions.DependencyInjection;
using Shared.Products;
using Shared.Products.Categories;

namespace Services.Products
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddProductServices(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            return services;
        }
    }
}
