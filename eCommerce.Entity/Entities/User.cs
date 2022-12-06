using eCommerce.Core.Entities;

namespace Database.Models
{
    public class User : EntityBase
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public DateTime DateBirth { get; set; }
        public string? Address { get; set; }

        public IEnumerable<Comment> Comments { get; set; }

        public IEnumerable<ShoppingSession> ShoppingSessions { get; set; }
    }
}
