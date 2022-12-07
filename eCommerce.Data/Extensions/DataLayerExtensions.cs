using eCommerce.Data.Context;
using eCommerce.Data.Repository.Abstractions;
using eCommerce.Data.Repository.Concretes;
using eCommerce.Data.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eCommerce.Data.Extensions
{
    public static class DataLayerExtensions
    {
        public static IServiceCollection LoadDataLayerExtensions(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddDbContext<AppDbContext>(p => p.UseSqlServer(configuration.GetConnectionString("default")));
            return services;
        }
    }
}
