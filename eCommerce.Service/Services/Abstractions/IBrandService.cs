using eCommerce.Entity.ViewModels.Brand;

namespace eCommerce.Service.Services.Abstractions
{
    public interface IBrandService
    {
        Task<IEnumerable<BrandViewModel>> GetAllBrandsNonDeletedAsync();
        Task<IEnumerable<BrandViewModel>> GetAllBrandsDeletedAsync();
        Task DeleteBrandAsync(Guid Id);
        Task<BrandViewModel> GetBrandByGuidAsync(Guid Id);
        Task UpdateBrandAsync(UpdateBrandViewModel viewModel);
        Task AddBrandAsync(AddBrandViewModel viewModel);
        Task RestoreBrandAsync(Guid id);
    }
}
