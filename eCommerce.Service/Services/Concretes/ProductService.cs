using AutoMapper;
using eCommerce.Data.UnitOfWorks;
using eCommerce.Entity.Entities;
using eCommerce.Entity.ViewModels.Brand;
using eCommerce.Entity.ViewModels.Category;
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

        public async Task AddProductAsync(AddProductViewModel viewModel)
        {
            var mappedProduct = mapper.Map<Product>(viewModel);
            await unitOfWork.GetRepository<Product>().AddAsync(mappedProduct);
            await unitOfWork.SaveAsync();
        }

        public async Task DeleteProductAsync(Guid id)
        {
            var product = await unitOfWork.GetRepository<Product>().GetByGuidAsync(id);
            product.IsDeleted = true;
            await unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<ProductViewModel>> GetAllProductsToBrandNonDeletedAsync(BrandViewModel viewModel)
        {
            var products = await unitOfWork.GetRepository<Product>().GetAllAsync(p => !p.IsDeleted && p.BrandId == viewModel.Id);
            var mappedProducts = mapper.Map<IEnumerable<ProductViewModel>>(products);
            return mappedProducts;
        }

        public async Task<IEnumerable<ProductViewModel>> GetAllProductsToCategoryNonDeletedAsync(CategoryViewModel viewModel)
        {
            var products = await unitOfWork.GetRepository<Product>().GetAllAsync(p => !p.IsDeleted && p.CategoryId == viewModel.Id);
            var mappedProducts = mapper.Map<IEnumerable<ProductViewModel>>(products);
            return mappedProducts;
        }

        public async Task<IEnumerable<ProductViewModel>> GetAllProductsWithBrandAndCategoryDeletedAsync()
        {
            var products = await unitOfWork.GetRepository<Product>().GetAllAsync(p => p.IsDeleted, p => p.Brand, p => p.Category);
            var mapped = mapper.Map<IEnumerable<ProductViewModel>>(products);
            return mapped;
        }

        public async Task<IEnumerable<ProductViewModel>> GetAllProductsWithBrandAndCategoryNonDeletedAsync()
        {
            var products = await unitOfWork.GetRepository<Product>().GetAllAsync(p => !p.IsDeleted, p => p.Brand, p => p.Category);
            var mapped = mapper.Map<IEnumerable<ProductViewModel>>(products);
            return mapped;
        }

        public async Task<ProductViewModel> GetProductByGuidAsync(Guid id)
        {
            var product = await unitOfWork.GetRepository<Product>().GetByGuidAsync(id);
            product.Category = await unitOfWork.GetRepository<Category>().GetByGuidAsync(product.CategoryId);
            product.Brand = await unitOfWork.GetRepository<Brand>().GetByGuidAsync(product.BrandId);
            var mappedProduct = mapper.Map<ProductViewModel>(product);
            return mappedProduct;
        }

        public async Task RestoreProductAsync(Guid id)
        {
            var product = await unitOfWork.GetRepository<Product>().GetByGuidAsync(id);
            product.IsDeleted = false;
            await unitOfWork.SaveAsync();
        }

        public async Task UpdateProductAsync(UpdateProductViewModel viewModel)
        {
            var product = await unitOfWork.GetRepository<Product>().GetByGuidAsync(viewModel.Id);
            product.Name = viewModel.Name;
            product.Description = viewModel.Description;
            product.Price = viewModel.Price;
            product.Quantity= viewModel.Quantity;
            product.BrandId = viewModel.BrandId;
            product.CategoryId = viewModel.CategoryId;
            await unitOfWork.SaveAsync();
        }
    }
}
