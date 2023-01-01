using AutoMapper;
using eCommerce.Data.UnitOfWorks;
using eCommerce.Entity.Entities;
using eCommerce.Entity.ViewModels.Product;
using eCommerce.Service.Services.Abstractions;

namespace eCommerce.Service.Services.Concretes
{
    public class CartService : ICartService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CartService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task AddProductToCartAsync(Guid productId, Guid sessionId)
        {
            var cart = new Cart() { ProductId = productId, ShoppingSessionId = sessionId };
            var product = await unitOfWork.GetRepository<Product>().GetByGuidAsync(productId);
            product.Quantity -= 1;
            await unitOfWork.GetRepository<Cart>().AddAsync(cart);
            await unitOfWork.SaveAsync();
        }

        public async Task<bool> AnyProductInCartAsync(Guid sessionId)
        {
            var result = await unitOfWork.GetRepository<Cart>().AnyAsync(p => p.ShoppingSessionId == sessionId && !p.IsDeleted);
            return result;
        }

        public async Task DeleteProductFromCartAsync(Guid productId, Guid sessionId)
        {
            var cart = await unitOfWork.GetRepository<Cart>().GetAsync(p => p.ProductId == productId && p.ShoppingSessionId == sessionId && !p.IsDeleted);
            cart.IsDeleted = true;
            var product = await unitOfWork.GetRepository<Product>().GetByGuidAsync(cart.ProductId);
            product.Quantity += 1;
            await unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<ProductViewModel>> GetProductsAsync(Guid sessionId)
        {
            var session = await unitOfWork.GetRepository<ShoppingSession>().GetAsync(p => p.Id == sessionId, p => p.Carts);
            session.Carts = await unitOfWork.GetRepository<Cart>().GetAllAsync(p => p.ShoppingSessionId == sessionId && !p.IsDeleted, p => p.Product, p => p.Product.Brand, p => p.Product.Category, p => p.Product.ProductImages);
            var products = session.Carts.Select(p => p.Product);
            foreach (var product in products)
            {
                product.ProductImages = await unitOfWork.GetRepository<ProductImage>().GetAllAsync(p => p.ProductId == product.Id, i => i.Image);
            }
            var mappedProducts = mapper.Map<IEnumerable<ProductViewModel>>(products);
            return mappedProducts;
        }
        public async Task<double> GetTotalPrice(Guid sessionId)
        {
            var result = await unitOfWork.GetRepository<Cart>().SumAsync(p => p.Product.Price, s => s.ShoppingSessionId == sessionId);
            return result;
        }
    }
}
