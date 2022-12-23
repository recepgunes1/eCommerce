using eCommerce.Entity.Entities;
using System.ComponentModel.DataAnnotations;

namespace eCommerce.Entity.ViewModels.User
{
    public class UpdateUserViewModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/mm/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime DateBirth { get; set; }
        public string Address { get; set; }
        public Guid RoleId { get; set; }
        public IEnumerable<Role>? Roles { get; set; }
    }
}
