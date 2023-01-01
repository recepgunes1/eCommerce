using AutoMapper;
using eCommerce.Data.UnitOfWorks;
using eCommerce.Entity.Entities;
using eCommerce.Entity.ViewModels.ShoppingSession;
using eCommerce.Entity.ViewModels.User;
using eCommerce.Service.Services.Abstractions;

namespace eCommerce.Service.Services.Concretes
{
    public class ShoppingSessionService : IShoppingSessionService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly User currentUser;

        public ShoppingSessionService(IUnitOfWork unitOfWork, IUserService userService, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            currentUser = userService.GetAuthenticatedUserAsync<User>().Result;
        }

        public async Task CloseSessionAsync()
        {
            var session = await unitOfWork.GetRepository<ShoppingSession>().GetAsync(p => p.UserId == currentUser.Id && p.IsActive);
            if (session != null)
            {
                session.IsActive = false;
                await unitOfWork.SaveAsync();
            }
        }

        public async Task CompleteShoppingAsync(Guid id)
        {
            var session = await unitOfWork.GetRepository<ShoppingSession>().GetByGuidAsync(id);
            session.IsActive = false;
            session.IsCompleted = true;
            await unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<ShoppingSessionViewModel>> GetAllOldOrdersAsync()
        {
            var sessions = await unitOfWork.GetRepository<ShoppingSession>().GetAllAsync(p => !p.IsDeleted && !p.IsActive && p.IsCompleted, c => c.Carts);
            foreach (var session in sessions)
            {
                session.Carts = await unitOfWork.GetRepository<Cart>().GetAllAsync(p => p.ShoppingSessionId == session.Id && !p.IsDeleted, p => p.Product);
                foreach (var cart in session.Carts)
                {
                    cart.Product.Brand = await unitOfWork.GetRepository<Brand>().GetAsync(p => p.Id == cart.Product.BrandId);
                }
            }
            var mappedSessions = mapper.Map<IEnumerable<ShoppingSessionViewModel>>(sessions);
            return mappedSessions;
        }

        public async Task<ShoppingSessionViewModel> GetCurrentShoppingSessionAsync()
        {
            var session = await unitOfWork.GetRepository<ShoppingSession>().GetAsync(p => p.UserId == currentUser.Id && p.IsActive && !p.IsCompleted && !p.IsDeleted, c => c.Carts);
            if (session == null)
            {
                session = await StartSessionAsync();
            }
            var mappedSession = mapper.Map<ShoppingSessionViewModel>(session);
            return mappedSession;
        }

        public async Task<IEnumerable<ShoppingSessionViewModel>> GetOldOrdersAsync(Guid userId)
        {

            var sessions = await unitOfWork.GetRepository<ShoppingSession>().GetAllAsync(p => p.UserId == userId && !p.IsDeleted && !p.IsActive && p.IsCompleted, c => c.Carts);
            foreach (var session in sessions)
            {
                session.Carts = await unitOfWork.GetRepository<Cart>().GetAllAsync(p => p.ShoppingSessionId == session.Id && !p.IsDeleted, p => p.Product);
                foreach (var cart in session.Carts)
                {
                    cart.Product.Brand = await unitOfWork.GetRepository<Brand>().GetAsync(p => p.Id == cart.Product.BrandId);
                }
            }
            var mappedSessions = mapper.Map<IEnumerable<ShoppingSessionViewModel>>(sessions);
            return mappedSessions;
        }

        public async Task<SimpleUserViewModel> GetOwnerByIdAsync(Guid id)
        {
            var user = await unitOfWork.GetRepository<User>().GetByGuidAsync(id);
            var mappedUser = mapper.Map<SimpleUserViewModel>(user);
            return mappedUser;
        }

        public async Task<ShoppingSession> StartSessionAsync()
        {
            var session = new ShoppingSession() { UserId = currentUser.Id, IsActive = true };
            await unitOfWork.GetRepository<ShoppingSession>().AddAsync(session);
            await unitOfWork.SaveAsync();
            return session;
        }
    }
}
