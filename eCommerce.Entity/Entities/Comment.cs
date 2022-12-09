using eCommerce.Core.Entities;

namespace eCommerce.Entity.Entities
{
    public class Comment : EntityBase
    {
        public string? Content { get; set; }

        public bool IsVisible { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
