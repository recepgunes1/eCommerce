using eCommerce.Entity.ViewModels.Brand;

namespace eCommerce.Service.Services.Abstractions
{
    public interface IBrandService
    {
        Task<IEnumerable<BrandViewModel>> GetAllBrandsNonDeletedAsync();
        Task<IEnumerable<BrandViewModel>> GetAllBrandsDeletedAsync();
        Task<IEnumerable<BrandViewModel>> GetSuggestedBrandsAsync();
        Task DeleteBrandAsync(Guid id);
        Task<BrandViewModel> GetBrandByGuidAsync(Guid id);
        Task UpdateBrandAsync(UpdateBrandViewModel viewModel);
        Task AddBrandAsync(AddBrandViewModel viewModel);
        Task RestoreBrandAsync(Guid id);
    }
}
