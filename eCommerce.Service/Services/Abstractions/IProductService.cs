using eCommerce.Entity.ViewModels.Brand;
using eCommerce.Entity.ViewModels.Category;
using eCommerce.Entity.ViewModels.Product;

namespace eCommerce.Service.Services.Abstractions
{
    public interface IProductService
    {
        Task AddProductAsync(AddProductViewModel viewModel);
        Task<IEnumerable<ProductViewModel>> GetAllProductsWithBrandAndCategoryToBrandNonDeletedAsync(BrandViewModel viewModel);
        Task<IEnumerable<ProductViewModel>> GetAllProductsWithBrandAndCategoryToCategoryNonDeletedAsync(CategoryViewModel viewModel);
        Task<IEnumerable<ProductViewModel>> GetAllProductsWithBrandAndCategoryNonDeletedAsync();
        Task<IEnumerable<ProductViewModel>> GetAllProductsWithBrandAndCategoryDeletedAsync();
        Task<IEnumerable<ProductViewModel>> GetSuggestedProductsAsync();
        Task DeleteProductAsync(Guid id);
        Task<ProductViewModel> GetProductByGuidAsync(Guid id);
        Task<ProductWithCommentsViewModel> GetProductWithCommentsByGuidAsync(Guid id);
        Task UpdateProductAsync(UpdateProductViewModel viewModel);
        Task RestoreProductAsync(Guid id);
    }
}
