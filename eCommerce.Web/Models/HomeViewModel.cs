using eCommerce.Entity.ViewModels.Category;

namespace eCommerce.Web.Models
{
    public class HomeViewModel
    {
        public IEnumerable<CategoryViewModel> ParentCategories { get; set; }
    }
}
