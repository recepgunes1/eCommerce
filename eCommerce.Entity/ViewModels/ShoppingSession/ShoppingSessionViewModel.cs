using eCommerce.Entity.ViewModels.Product;

namespace eCommerce.Entity.ViewModels.ShoppingSession
{
    public class ShoppingSessionViewModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public IEnumerable<SimpleProductViewModel> Products { get; set; }
    }
}
