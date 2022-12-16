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
            CreateMap<Product, ProductViewModel>().ReverseMap();
            CreateMap<Product, SimpleProductViewModel>().ReverseMap();
            CreateMap<ProductViewModel, UpdateProductViewModel>().ReverseMap();
        }
    }
}
