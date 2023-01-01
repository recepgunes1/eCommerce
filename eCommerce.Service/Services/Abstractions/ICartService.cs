using eCommerce.Entity.ViewModels.Product;

namespace eCommerce.Service.Services.Abstractions
{
    public interface ICartService
    {
        Task AddProductToCartAsync(Guid productId, Guid sessionId);
        Task DeleteProductFromCartAsync(Guid productId, Guid sessionId);
        Task<bool> AnyProductInCartAsync(Guid sessionId);
        Task<IEnumerable<ProductViewModel>> GetProductsAsync(Guid sessionId);
        Task<double> GetTotalPrice(Guid sessionId);
    }
}
