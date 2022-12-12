using AutoMapper;
using eCommerce.Entity.Entities;
using eCommerce.Entity.ViewModels.Product;

namespace eCommerce.Service.AutoMapper.Products
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductViewModel>().ReverseMap();
        }
    }
}
