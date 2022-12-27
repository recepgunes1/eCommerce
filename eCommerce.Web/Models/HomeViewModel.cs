using eCommerce.Entity.ViewModels.Brand;
using eCommerce.Entity.ViewModels.Carousel;
using eCommerce.Entity.ViewModels.Category;
using eCommerce.Entity.ViewModels.Product;

namespace eCommerce.Web.Models
{
    public class HomeViewModel
    {
        public IEnumerable<CategoryViewModel> ParentCategories { get; set; }
        public IEnumerable<CarouselViewModel> Slides { get; set; }
        public IEnumerable<BrandViewModel> SuggestedBrands { get; set; }
        public IEnumerable<ProductViewModel> SuggestedProducts { get; set; }
    }
}
