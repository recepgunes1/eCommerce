using System.ComponentModel.DataAnnotations;

namespace eCommerce.Entity.ViewModels.User
{
    public class ChangeUserPasswordViewModel
    {
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }


        [DataType(DataType.Password)]
        [Compare("Password")]
        [Required]
        public string PasswordConfirm { get; set; }

        [Required]
        public string CurrentPassowrd { get; set; }
    }
}
