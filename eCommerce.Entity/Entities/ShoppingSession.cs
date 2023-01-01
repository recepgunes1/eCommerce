using eCommerce.Core.Entities;

namespace eCommerce.Entity.Entities
{
    public class ShoppingSession : EntityBase
    {
        public bool IsActive { get; set; }
        public bool IsCompleted { get; set; }
        public Guid UserId { get; set; }
        public User? User { get; set; }

        public IEnumerable<Cart> Carts { get; set; }
    }
}
