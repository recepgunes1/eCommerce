using eCommerce.Entity.ViewModels.Brand;

namespace eCommerce.Entity.ViewModels.Product
{
    public class ProductViewModel
    {
        public string Name { get; set; }
        public BrandViewModel Brand { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
    }
}
