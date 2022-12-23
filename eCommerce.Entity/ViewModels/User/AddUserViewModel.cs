using eCommerce.Entity.Entities;
using System.ComponentModel.DataAnnotations;

namespace eCommerce.Entity.ViewModels.User
{
    public class AddUserViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string PasswordConfirm { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateBirth { get; set; }
        public string Address { get; set; }
        public Guid RoleId { get; set; }
        public IEnumerable<Role>? Roles { get; set; }
    }
}
