using eCommerce.Entity.ViewModels.Brand;
using eCommerce.Entity.ViewModels.Category;
using eCommerce.Entity.ViewModels.Image;
using Microsoft.AspNetCore.Http;

namespace eCommerce.Entity.ViewModels.Product
{
    public class UpdateProductViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public Guid CategoryId { get; set; }
        public Guid BrandId { get; set; }
        public IFormFile? Photo { get; set; }
        public SimpleCategoryViewModel Category { get; set; }
        public SimpleBrandViewModel Brand { get; set; }
        public IList<DeleteImageViewModel> Images { get; set; }
        public IEnumerable<SimpleCategoryViewModel> Categories { get; set; }
        public IEnumerable<SimpleBrandViewModel> Brands { get; set; }
    }
}
