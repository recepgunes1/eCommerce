using eCommerce.Entity.ViewModels.Product;

namespace eCommerce.Service.Services.Abstractions
{
    public interface IProductService
    {
        Task<List<ProductViewModel>> GetAllProductsWithBrandAsync();
    }
}
