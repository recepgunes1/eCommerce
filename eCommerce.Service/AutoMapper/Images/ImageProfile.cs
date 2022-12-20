using AutoMapper;
using eCommerce.Entity.Entities;
using eCommerce.Entity.ViewModels.Image;

namespace eCommerce.Service.AutoMapper.Images
{
    public class ImageProfile : Profile
    {
        public ImageProfile()
        {
            CreateMap<Image, UploadImageViewModel>().ReverseMap();
            CreateMap<Image, ImageViewModel>().ReverseMap();
            CreateMap<ImageViewModel, DeleteImageViewModel>().ReverseMap();
        }
    }
}
