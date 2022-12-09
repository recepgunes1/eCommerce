using eCommerce.Entity.Entities;

namespace eCommerce.Service.Services.Abstractions
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProductsAsync();
    }
}
