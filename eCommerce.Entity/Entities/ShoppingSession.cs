using eCommerce.Core.Entities;

namespace Database.Models
{
    public class ShoppingSession : EntityBase
    {
        public int Status { get; set; }

        public Guid UserId { get; set; }
        public User? User { get; set; }

        public IEnumerable<Card> Cards { get; set; }
    }
}
