using System.ComponentModel.DataAnnotations;

namespace eCommerce.Entity.ViewModels.Auth
{
    public class SignUpViewModel
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
    }
}
