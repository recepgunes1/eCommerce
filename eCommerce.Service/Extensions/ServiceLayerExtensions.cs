using eCommerce.Service.Services.Abstractions;
using eCommerce.Service.Services.Concretes;
using Microsoft.Extensions.DependencyInjection;

namespace eCommerce.Service.Extensions
{
    public static class ServiceLayerExtensions
    {
        public static IServiceCollection LoadServiceLayerExtensions(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            return services;
        }
    }
}
