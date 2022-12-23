using System.ComponentModel.DataAnnotations;

namespace eCommerce.Entity.ViewModels.User
{
    public class LockoutUserViewModel
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/mm/yyyy}", ApplyFormatInEditMode = false)]
        public DateTimeOffset LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; } = true;
    }
}
