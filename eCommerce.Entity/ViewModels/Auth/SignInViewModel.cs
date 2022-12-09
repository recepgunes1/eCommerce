using System.ComponentModel.DataAnnotations;

namespace WebUI.Models.ViewModels.Account
{
    public class SignInViewModel
    {
        [Required, EmailAddress]
        public string EMail { get; set; }

        [Required]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
