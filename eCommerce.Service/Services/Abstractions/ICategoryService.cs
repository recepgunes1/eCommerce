using eCommerce.Entity.ViewModels.Category;

namespace eCommerce.Service.Services.Abstractions
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryViewModel>> GetAllCategoriesNonDeletedAsync();
        Task<IEnumerable<CategoryViewModel>> GetAllCategoriesDeletedAsync();
        Task<IEnumerable<CategoryViewModel>> GetAllSubCategoriesNonDeletedAsync();
        Task<IEnumerable<CategoryViewModel>> GetAllParentCategoriesNonDeletedAsync();
        Task<IEnumerable<SimpleCategoryViewModel>> GetAllSubCategoriesToParentGuidNonDeletedAsync(Guid id);
        Task<CategoryViewModel> GetCategoryByGuidAsync(Guid id);
        Task AddCategoryAsync(AddCategoryViewModel viewModel);
        Task DeleteCategoryAsync(Guid id);
        Task UpdateCategoryAsync(UpdateCategoryViewModel viewModel);
        Task RestoreCategoryAsync(Guid id);
        Task<(int ParentDeleted, int ParentNonDeleted, int ChildDeleted, int ChildNonDeleted)> CountCategoriesAsync();
    }
}
