﻿using AutoMapper;
using eCommerce.Data.UnitOfWorks;
using eCommerce.Entity.Entities;
using eCommerce.Entity.ViewModels.Brand;
using eCommerce.Service.Services.Abstractions;

namespace eCommerce.Service.Services.Concretes
{
    public class BrandService : IBrandService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public BrandService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task AddBrandAsync(AddBrandViewModel viewModel)
        {
            var mappedBrand = mapper.Map<Brand>(viewModel);
            await unitOfWork.GetRepository<Brand>().AddAsync(mappedBrand);
            await unitOfWork.SaveAsync();
        }

        public async Task DeleteBrandAsync(Guid Id)
        {
            var brand = await unitOfWork.GetRepository<Brand>().GetByGuidAsync(Id);
            brand.IsDeleted = true;
            await unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<BrandDetailedViewModel>> GetAllBrandsNonDeletedAsync()
        {
            var brands = await unitOfWork.GetRepository<Brand>().GetAllAsync(p => !p.IsDeleted);
            var mappedBrands = mapper.Map<IEnumerable<BrandDetailedViewModel>>(brands);
            return mappedBrands;
        }

        public async Task<BrandDetailedViewModel> GetBrandAsync(Guid Id)
        {
            var brand = await unitOfWork.GetRepository<Brand>().GetByGuidAsync(Id);
            var mappedBrand = mapper.Map<BrandDetailedViewModel>(brand);
            return mappedBrand;
        }

        public async Task UpdateBrandAsync(UpdateBrandViewModel viewModel)
        {
            var brand = await unitOfWork.GetRepository<Brand>().GetByGuidAsync(viewModel.Id);
            brand.Name = viewModel.Name;
            await unitOfWork.SaveAsync();
        }
    }
}