using AutoMapper;
using eCommerce.Entity.Entities;
using eCommerce.Entity.ViewModels.Comment;

namespace eCommerce.Service.AutoMapper.Comments
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<Comment, CommentViewModel>().ReverseMap();
        }
    }
}
