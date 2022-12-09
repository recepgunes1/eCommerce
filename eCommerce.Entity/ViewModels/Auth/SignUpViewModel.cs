using System.ComponentModel.DataAnnotations;

namespace WebUI.Models.ViewModels.Account
{
    public class SignUpViewModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        [EmailAddress]
        public string? EMail { get; set; }

        [DataType(DataType.Password)]
        public string? Password { get; set; }


        [DataType(DataType.Password)]
        [Compare("Password")]
        public string? PasswordConfirm { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateBirth { get; set; }

        public string? Address { get; set; }
        public bool TermsOfService { get; set; }
        public bool ReceiveMessages { get; set; }
    }
}
