using eCommerce.Core.Entities;

namespace eCommerce.Entity.Entities
{
    public class Product : EntityBase
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }

        public Guid CategoryId { get; set; }
        public Category Category { get; set; }

        public Guid BrandId { get; set; }
        public Brand Brand { get; set; }

        public IEnumerable<Comment> Comments { get; set; }
        public IEnumerable<ProductImage> ProductImages { get; set; }

    }
}
