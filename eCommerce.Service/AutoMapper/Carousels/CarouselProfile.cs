using AutoMapper;
using eCommerce.Entity.Entities;
using eCommerce.Entity.ViewModels.Carousel;

namespace eCommerce.Service.AutoMapper.Carousels
{
    public class CarouselProfile : Profile
    {
        public CarouselProfile()
        {
            CreateMap<Carousel, AddCarouselViewModel>().ReverseMap();
            CreateMap<Carousel, CarouselViewModel>().ReverseMap();
        }
    }
}
