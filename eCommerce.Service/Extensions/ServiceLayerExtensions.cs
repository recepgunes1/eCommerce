using eCommerce.Service.Services.Abstractions;
using eCommerce.Service.Services.Concretes;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace eCommerce.Service.Extensions
{
    public static class ServiceLayerExtensions
    {
        public static IServiceCollection LoadServiceLayerExtensions(this IServiceCollection services)
        {
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
