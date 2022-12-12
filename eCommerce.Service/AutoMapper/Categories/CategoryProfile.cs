using AutoMapper;
using eCommerce.Entity.Entities;
using eCommerce.Entity.ViewModels.Category;

namespace eCommerce.Service.AutoMapper.Categories
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryViewModel>().ReverseMap();
            CreateMap<Category, AddCategoryViewModel>().ReverseMap();
            CreateMap<Category, SimpleCategoryViewModel>().ReverseMap();
            CreateMap<Category, UpdateCategoryViewModel>().ReverseMap();
            CreateMap<CategoryViewModel, UpdateCategoryViewModel>().ReverseMap();
            CreateMap<CategoryViewModel, SimpleCategoryViewModel>().ReverseMap();
        }
    }
}
