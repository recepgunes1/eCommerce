using eCommerce.Entity.Entities;
using eCommerce.Entity.ViewModels.ShoppingSession;
using eCommerce.Entity.ViewModels.User;

namespace eCommerce.Service.Services.Abstractions
{
    public interface IShoppingSessionService
    {
        Task<ShoppingSession> StartSessionAsync();
        Task CloseSessionAsync();
        Task<ShoppingSessionViewModel> GetCurrentShoppingSessionAsync();
        Task CompleteShoppingAsync(Guid id);
        Task<IEnumerable<ShoppingSessionViewModel>> GetOldOrdersAsync(Guid userId);
        Task<IEnumerable<ShoppingSessionViewModel>> GetAllOldOrdersAsync();
        Task<SimpleUserViewModel> GetOwnerByIdAsync(Guid id);
    }
}
