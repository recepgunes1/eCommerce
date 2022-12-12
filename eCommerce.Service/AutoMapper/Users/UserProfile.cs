using AutoMapper;
using eCommerce.Entity.Entities;
using eCommerce.Entity.ViewModels.Auth;

namespace eCommerce.Service.AutoMapper.Users
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, SignInViewModel>().ReverseMap();
            CreateMap<User, SignUpViewModel>().ReverseMap();
        }
    }
}
