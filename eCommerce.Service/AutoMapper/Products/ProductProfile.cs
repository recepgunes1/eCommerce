using AutoMapper;
using eCommerce.Entity.Entities;
using eCommerce.Entity.ViewModels.Product;

namespace eCommerce.Service.AutoMapper.Products
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, AddProductViewModel>().ReverseMap();
            CreateMap<Product, ProductViewModel>()
                .ForMember(p => p.Images, o => o.MapFrom(p => p.ProductImages.Select(s => s.Image)))
                .ReverseMap();
            CreateMap<Product, ProductWithCommentsViewModel>()
                .ForMember(p => p.Images, o => o.MapFrom(p => p.ProductImages.Select(s => s.Image)))
                .ReverseMap();
            CreateMap<Product, SimpleProductViewModel>().ReverseMap();
            CreateMap<ProductViewModel, UpdateProductViewModel>().ReverseMap();
        }
    }
}
