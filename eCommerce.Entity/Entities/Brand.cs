using eCommerce.Core.Entities;

namespace eCommerce.Entity.Entities
{
    public class Brand : EntityBase
    {
        public string? Name { get; set; }
        public byte[]? Icon { get; set; }

        public IEnumerable<Product> Products { get; set; }
    }
}
