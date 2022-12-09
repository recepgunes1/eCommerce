using eCommerce.Data.UnitOfWorks;
using eCommerce.Entity.Entities;
using eCommerce.Service.Services.Abstractions;

namespace eCommerce.Service.Services.Concretes
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await unitOfWork.GetRepository<Product>().GetAllAsync();
        }
    }
}
