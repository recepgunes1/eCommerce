using AutoMapper;
using eCommerce.Entity.Entities;
using eCommerce.Entity.ViewModels.Auth;
using eCommerce.Entity.ViewModels.User;

namespace eCommerce.Service.AutoMapper.Users
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, SignInViewModel>().ReverseMap();
            CreateMap<User, SignUpViewModel>().ReverseMap();
            CreateMap<User, SimpleUserViewModel>().ReverseMap();
            CreateMap<User, AddUserViewModel>().ReverseMap();
            CreateMap<User, UserViewModel>().ReverseMap();
            CreateMap<User, UpdateUserViewModel>().ReverseMap();
            CreateMap<User, LockoutUserViewModel>().ReverseMap();
            CreateMap<UserViewModel, UpdateUserViewModel>().ReverseMap();
        }
    }
}
