using AutoMapper;
using eCommerce.Entity.Entities;
using eCommerce.Entity.ViewModels.Brand;

namespace eCommerce.Service.AutoMapper.Brands
{
    public class BrandProfile : Profile
    {
        public BrandProfile()
        {
            CreateMap<Brand, BrandViewModel>().ReverseMap();
            CreateMap<Brand, BrandDetailedViewModel>().ReverseMap();
            CreateMap<BrandDetailedViewModel, UpdateBrandViewModel>().ReverseMap();
            CreateMap<Brand, AddBrandViewModel>().ReverseMap();
        }
    }
}
