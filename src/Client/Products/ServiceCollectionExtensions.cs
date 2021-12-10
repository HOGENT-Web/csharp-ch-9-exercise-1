using Client.Products.Categories;
using Microsoft.Extensions.DependencyInjection;
using Shared.Products;
using Shared.Products.Categories;

namespace Client.Products
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddClientsideProductServices(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            return services;
        }
    }
}
