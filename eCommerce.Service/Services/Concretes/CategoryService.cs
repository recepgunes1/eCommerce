using AutoMapper;
using eCommerce.Data.UnitOfWorks;
using eCommerce.Entity.Entities;
using eCommerce.Entity.ViewModels.Category;
using eCommerce.Service.Services.Abstractions;

namespace eCommerce.Service.Services.Concretes
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task AddCategoryAsync(AddCategoryViewModel viewModel)
        {
            var mappedCategory = mapper.Map<Category>(viewModel);
            await unitOfWork.GetRepository<Category>().AddAsync(mappedCategory);
            await unitOfWork.SaveAsync();
        }

        public async Task DeleteCategoryAsync(Guid id)
        {
            var category = await unitOfWork.GetRepository<Category>().GetByGuidAsync(id);
            category.IsDeleted = true;
            await unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<CategoryViewModel>> GetAllCategoriesDeletedAsync()
        {
            var categories = await unitOfWork.GetRepository<Category>().GetAllAsync(p => p.IsDeleted, p => p.ParentCategory);
            var mappedCategories = mapper.Map<IEnumerable<CategoryViewModel>>(categories);
            return mappedCategories;
        }

        public async Task<IEnumerable<CategoryViewModel>> GetAllCategoriesNonDeletedAsync()
        {
            var categories = await unitOfWork.GetRepository<Category>().GetAllAsync(p => !p.IsDeleted, p => p.ParentCategory);
            var mappedCategories = mapper.Map<IEnumerable<CategoryViewModel>>(categories);
            return mappedCategories;
        }

        public async Task<IEnumerable<CategoryViewModel>> GetAllParentCategoriesNonDeletedAsync()
        {
            var categories = await unitOfWork.GetRepository<Category>().GetAllAsync(p => !p.IsDeleted && p.ParentCategory == null);
            var mappedCategories = mapper.Map<IEnumerable<CategoryViewModel>>(categories);
            return mappedCategories;
        }

        public async Task<IEnumerable<CategoryViewModel>> GetAllSubCategoriesNonDeletedAsync()
        {
            var categories = await unitOfWork.GetRepository<Category>().GetAllAsync(p => !p.IsDeleted && p.ParentCategoryId != null);
            var mappedCategories = mapper.Map<IEnumerable<CategoryViewModel>>(categories);
            return mappedCategories;
        }
        public async Task<IEnumerable<SimpleCategoryViewModel>> GetAllSubCategoriesToParentGuidNonDeletedAsync(Guid id)
        {
            var categories = await unitOfWork.GetRepository<Category>().GetAllAsync(p => !p.IsDeleted && p.ParentCategoryId == id);
            var mappedCategories = mapper.Map<IEnumerable<SimpleCategoryViewModel>>(categories);
            return mappedCategories;
        }

        public async Task<CategoryViewModel> GetCategoryByGuidAsync(Guid id)
        {
            var category = await unitOfWork.GetRepository<Category>().GetAsync(p => p.Id == id, pc => pc.ParentCategory); ;
            var mappedCategory = mapper.Map<CategoryViewModel>(category);
            return mappedCategory;
        }

        public async Task RestoreCategoryAsync(Guid id)
        {
            var category = await unitOfWork.GetRepository<Category>().GetByGuidAsync(id);
            category.IsDeleted = false;
            await unitOfWork.SaveAsync();
        }

        public async Task UpdateCategoryAsync(UpdateCategoryViewModel viewModel)
        {
            var category = await unitOfWork.GetRepository<Category>().GetByGuidAsync(viewModel.Id);
            category.Name = viewModel.Name;
            category.ParentCategoryId = viewModel.NewParentCategoryId == Guid.Empty ? null : viewModel.NewParentCategoryId;
            await unitOfWork.SaveAsync();
        }
    }
}
