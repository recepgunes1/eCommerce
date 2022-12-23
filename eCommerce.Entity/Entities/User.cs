using eCommerce.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace eCommerce.Entity.Entities
{
    public class User : IdentityUser<Guid>, IEntityBase
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateBirth { get; set; }
        public string Address { get; set; }

        public IEnumerable<Comment> Comments { get; set; }

        public IEnumerable<ShoppingSession> ShoppingSessions { get; set; }
    }
}
