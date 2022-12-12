using eCommerce.Entity.ViewModels.Brand;

namespace eCommerce.Service.Services.Abstractions
{
    public interface IBrandService
    {
        Task<IEnumerable<BrandDetailedViewModel>> GetAllBrandsNonDeletedAsync();
        Task DeleteBrandAsync(Guid Id);
        Task<BrandDetailedViewModel> GetBrandAsync(Guid Id);
        Task UpdateBrandAsync(UpdateBrandViewModel viewModel);
        Task AddBrandAsync(AddBrandViewModel viewModel);
    }
}
