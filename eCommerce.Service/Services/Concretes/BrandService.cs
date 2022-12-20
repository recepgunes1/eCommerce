using AutoMapper;
using eCommerce.Data.UnitOfWorks;
using eCommerce.Entity.Entities;
using eCommerce.Entity.ViewModels.Brand;
using eCommerce.Service.Helpers.Images;
using eCommerce.Service.Services.Abstractions;

namespace eCommerce.Service.Services.Concretes
{
    public class BrandService : IBrandService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IImageHelper imageHelper;

        public BrandService(IUnitOfWork unitOfWork, IMapper mapper, IImageHelper imageHelper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.imageHelper = imageHelper;
        }

        public async Task AddBrandAsync(AddBrandViewModel viewModel)
        {
            var image = await imageHelper.UploadAsync(viewModel.Photo, "brands");
            var mappedImage = mapper.Map<Image>(image);
            await unitOfWork.GetRepository<Image>().AddAsync(mappedImage);

            var mappedBrand = mapper.Map<Brand>(viewModel);
            mappedBrand.ImageId = mappedImage.Id;
            await unitOfWork.GetRepository<Brand>().AddAsync(mappedBrand);

            await unitOfWork.SaveAsync();
        }

        public async Task DeleteBrandAsync(Guid id)
        {
            var brand = await unitOfWork.GetRepository<Brand>().GetByGuidAsync(id);
            brand = await unitOfWork.GetRepository<Brand>().GetAsync(p => p.Id == brand.Id, p => p.Products);
            if (brand.ImageId != null)
            {
                var image = await unitOfWork.GetRepository<Image>().GetByGuidAsync(brand.ImageId.Value);
                image.IsDeleted = true;
            }
            if (brand.Products.Any())
            {
                foreach (var product in brand.Products)
                {
                    product.IsDeleted = true;
                }
            }
            brand.IsDeleted = true;
            await unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<BrandViewModel>> GetAllBrandsDeletedAsync()
        {
            var brands = await unitOfWork.GetRepository<Brand>().GetAllAsync(p => p.IsDeleted);
            var mappedBrands = mapper.Map<IEnumerable<BrandViewModel>>(brands);
            return mappedBrands;
        }

        public async Task<IEnumerable<BrandViewModel>> GetAllBrandsNonDeletedAsync()
        {
            var brands = await unitOfWork.GetRepository<Brand>().GetAllAsync(p => !p.IsDeleted, i => i.Image);
            var mappedBrands = mapper.Map<IEnumerable<BrandViewModel>>(brands);
            return mappedBrands;
        }

        public async Task<BrandViewModel> GetBrandByGuidAsync(Guid id)
        {
            var brand = await unitOfWork.GetRepository<Brand>().GetAsync(p => p.Id == id, i => i.Image);
            var mappedBrand = mapper.Map<BrandViewModel>(brand);
            return mappedBrand;
        }

        public async Task RestoreBrandAsync(Guid id)
        {
            var brand = await unitOfWork.GetRepository<Brand>().GetByGuidAsync(id);
            brand = await unitOfWork.GetRepository<Brand>().GetAsync(p => p.Id == brand.Id, p => p.Products);
            if (brand.ImageId != null)
            {
                var image = await unitOfWork.GetRepository<Image>().GetByGuidAsync(brand.ImageId.Value);
                image.IsDeleted = false;
            }
            if (brand.Products.Any())
            {
                foreach (var product in brand.Products)
                {
                    product.IsDeleted = false;
                }
            }
            brand.IsDeleted = false;
            await unitOfWork.SaveAsync();
        }

        public async Task UpdateBrandAsync(UpdateBrandViewModel viewModel)
        {
            var brand = await unitOfWork.GetRepository<Brand>().GetAsync(p => p.Id == viewModel.Id, i => i.Image);
            if (viewModel.Photo != null)
            {
                var oldImage = await unitOfWork.GetRepository<Image>().GetByGuidAsync(brand.ImageId.GetValueOrDefault());
                oldImage.IsDeleted = true;
                var newImage = await imageHelper.UploadAsync(viewModel.Photo, "brands");
                var mappedNewImage = mapper.Map<Image>(newImage);
                await unitOfWork.GetRepository<Image>().AddAsync(mappedNewImage);
                brand.ImageId = mappedNewImage.Id;
            }
            brand.Name = viewModel.Name;
            await unitOfWork.SaveAsync();
        }
    }
}
