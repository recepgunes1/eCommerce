using AutoMapper;
using eCommerce.Entity.Entities;
using eCommerce.Entity.ViewModels.Brand;

namespace eCommerce.Service.AutoMapper.Brands
{
    public class BrandProfile : Profile
    {
        public BrandProfile()
        {
            CreateMap<Brand, SimpleBrandViewModel>().ReverseMap();
            CreateMap<Brand, BrandViewModel>().ReverseMap();
            CreateMap<Brand, AddBrandViewModel>().ReverseMap();
            CreateMap<BrandViewModel, UpdateBrandViewModel>().ReverseMap();
            CreateMap<BrandViewModel, SimpleBrandViewModel>().ReverseMap();
        }
    }
}
