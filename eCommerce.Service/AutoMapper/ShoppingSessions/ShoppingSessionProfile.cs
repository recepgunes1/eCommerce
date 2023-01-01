using AutoMapper;
using eCommerce.Entity.Entities;
using eCommerce.Entity.ViewModels.ShoppingSession;

namespace eCommerce.Service.AutoMapper.ShoppingSessions
{
    public class ShoppingSessionProfile : Profile
    {
        public ShoppingSessionProfile()
        {
            CreateMap<ShoppingSession, ShoppingSessionViewModel>()
                .ForMember(p => p.Products, o => o.MapFrom(t => t.Carts.Select(i => i.Product)))
                .ReverseMap();
        }
    }
}
