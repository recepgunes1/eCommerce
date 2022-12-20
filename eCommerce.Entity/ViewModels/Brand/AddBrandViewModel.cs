using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace eCommerce.Entity.ViewModels.Brand
{
    public class AddBrandViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public IFormFile Photo { get; set; }
    }
}
