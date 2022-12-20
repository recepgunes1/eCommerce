using AutoMapper;
using eCommerce.Data.UnitOfWorks;
using eCommerce.Entity.Entities;
using eCommerce.Entity.ViewModels.Brand;
using eCommerce.Entity.ViewModels.Category;
using eCommerce.Entity.ViewModels.Product;
using eCommerce.Service.Helpers.Images;
using eCommerce.Service.Services.Abstractions;

namespace eCommerce.Service.Services.Concretes
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IImageHelper imageHelper;
        private readonly IMapper mapper;

        public ProductService(IUnitOfWork unitOfWork, IImageHelper imageHelper, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.imageHelper = imageHelper;
            this.mapper = mapper;
        }

        public async Task AddProductAsync(AddProductViewModel viewModel)
        {
            var uploadImageViewModel = await imageHelper.UploadAsync(viewModel.Photo, "products");
            var mappedImage = mapper.Map<Image>(uploadImageViewModel);
            var mappedProduct = mapper.Map<Product>(viewModel);
            await unitOfWork.GetRepository<Product>().AddAsync(mappedProduct);
            await unitOfWork.GetRepository<Image>().AddAsync(mappedImage);
            await unitOfWork.GetRepository<ProductImage>().AddAsync(new() { ImageId = mappedImage.Id, ProductId = mappedProduct.Id });
            await unitOfWork.SaveAsync();
        }

        public async Task DeleteProductAsync(Guid id)
        {
            var product = await unitOfWork.GetRepository<Product>().GetByGuidAsync(id);
            product.IsDeleted = true;
            await unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<ProductViewModel>> GetAllProductsWithBrandAndCategoryToBrandNonDeletedAsync(BrandViewModel viewModel)
        {
            var products = await unitOfWork.GetRepository<Product>().GetAllAsync(p => !p.IsDeleted && p.BrandId == viewModel.Id, b => b.Brand, c => c.Category);
            var mappedProducts = mapper.Map<IEnumerable<ProductViewModel>>(products);
            return mappedProducts;
        }

        public async Task<IEnumerable<ProductViewModel>> GetAllProductsWithBrandAndCategoryToCategoryNonDeletedAsync(CategoryViewModel viewModel)
        {
            var products = await unitOfWork.GetRepository<Product>().GetAllAsync(p => !p.IsDeleted && p.CategoryId == viewModel.Id, b => b.Brand, c => c.Category);
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
            var product = await unitOfWork.GetRepository<Product>().GetAsync(p => p.Id == id, c => c.Category, b => b.Brand, i => i.ProductImages);
            product.ProductImages = await unitOfWork.GetRepository<ProductImage>().GetAllAsync(p => p.ProductId == product.Id && !p.Image.IsDeleted, p => p.Image);
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
            var product = await unitOfWork.GetRepository<Product>().GetAsync(p => p.Id == viewModel.Id, p => p.ProductImages);
            product.ProductImages = await unitOfWork.GetRepository<ProductImage>().GetAllAsync(p => p.ProductId == product.Id, p => p.Image);
            foreach (var productImage in viewModel.Images.SelectMany(i => product.ProductImages.Where(pi => i.IsDeleted && pi.Image.NameWithPath == i.NameWithPath)))
            {
                productImage.IsDeleted = true;
                productImage.Image.IsDeleted = true;
            }
            if (viewModel.Photo != null)
            {
                var image = await imageHelper.UploadAsync(viewModel.Photo, "products");
                var mappedImage = mapper.Map<Image>(image);
                await unitOfWork.GetRepository<Image>().AddAsync(mappedImage);
                await unitOfWork.GetRepository<ProductImage>().AddAsync(new ProductImage { ProductId = product.Id, ImageId = mappedImage.Id });
            }
            product.Name = viewModel.Name;
            product.Description = viewModel.Description;
            product.Price = viewModel.Price;
            product.Quantity = viewModel.Quantity;
            product.BrandId = viewModel.BrandId;
            product.CategoryId = viewModel.CategoryId;
            await unitOfWork.SaveAsync();
        }
    }
}
