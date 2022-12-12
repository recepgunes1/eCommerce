using eCommerce.Entity.ViewModels.Category;

namespace eCommerce.Service.Services.Abstractions
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryViewModel>> GetAllCategoriesNonDeletedAsync();
        Task<IEnumerable<CategoryViewModel>> GetParentCategoriesNonDeletedAsync();
        Task<IEnumerable<CategoryViewModel>> GetSubCategoriesNonDeletedAsync(Guid id);
        Task<CategoryViewModel> GetCategoryByGuidAsync(Guid id);
        Task AddCategoryAsync(AddCategoryViewModel viewModel);
        Task DeleteCategoryAsync(Guid id);
        Task UpdateCategoryAsync(UpdateCategoryViewModel viewModel);
    }
}
