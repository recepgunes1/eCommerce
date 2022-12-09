using eCommerce.Core.Entities;

namespace eCommerce.Entity.Entities
{
    public class ShoppingSession : EntityBase
    {
        public int Status { get; set; }

        public Guid UserId { get; set; }
        public User? User { get; set; }

        public IEnumerable<Cart> Carts { get; set; }
    }
}
