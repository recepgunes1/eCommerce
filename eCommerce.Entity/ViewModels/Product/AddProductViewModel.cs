using eCommerce.Entity.ViewModels.Brand;
using eCommerce.Entity.ViewModels.Category;
using Microsoft.AspNetCore.Http;

namespace eCommerce.Entity.ViewModels.Product
{
    public class AddProductViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public Guid CategoryId { get; set; }
        public Guid BrandId { get; set; }
        public IFormFile Photo { get; set; }
        public IEnumerable<SimpleCategoryViewModel> Categories { get; set; }
        public IEnumerable<SimpleBrandViewModel> Brands { get; set; }
    }
}
