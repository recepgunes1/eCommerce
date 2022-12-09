using eCommerce.Core.Entities;

namespace eCommerce.Entity.Entities
{
    public class ProductImage : EntityBase
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public Guid ImageId { get; set; }
        public Image Image { get; set; }

    }
}
