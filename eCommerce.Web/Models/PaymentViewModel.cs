using System.ComponentModel.DataAnnotations;

namespace eCommerce.Web.Models
{
    public class PaymentViewModel
    {
        public string HolderName { get; set; }

        [Display(Name = "Card Number")]
        [Range(100000000000, 9999999999999999999, ErrorMessage = "must be between 12 and 19 digits")]
        public string CardNumber { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        [Range(000, 999, ErrorMessage = "must be 3 digits")]
        public int CVV { get; set; }
    }
}
