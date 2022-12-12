using AutoMapper;
using eCommerce.Data.UnitOfWorks;
using eCommerce.Entity.Entities;
using eCommerce.Entity.ViewModels.Product;
using eCommerce.Service.Services.Abstractions;

namespace eCommerce.Service.Services.Concretes
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<List<ProductViewModel>> GetAllProductsWithBrandAsync()
        {
            var products = await unitOfWork.GetRepository<Product>().GetAllAsync(null, p => p.Brand);
            var mapped = mapper.Map<List<ProductViewModel>>(products);
            return mapped;
        }
    }
}
