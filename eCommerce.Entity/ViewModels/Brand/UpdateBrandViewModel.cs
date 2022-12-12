using System.ComponentModel.DataAnnotations;

namespace eCommerce.Entity.ViewModels.Brand
{
    public class UpdateBrandViewModel
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
