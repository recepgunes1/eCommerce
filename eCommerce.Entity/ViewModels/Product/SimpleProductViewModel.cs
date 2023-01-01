using eCommerce.Entity.ViewModels.Brand;

namespace eCommerce.Entity.ViewModels.Product
{
    public class SimpleProductViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public SimpleBrandViewModel Brand { get; set; }
    }
}
