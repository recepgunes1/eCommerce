using eCommerce.Core.Entities;

namespace Database.Models
{
    public class ProductImage : EntityBase
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public Guid ImageId { get; set; }
        public Image Image { get; set; }

    }
}
