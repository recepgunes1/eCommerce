using eCommerce.Core.Entities;

namespace Database.Models
{
    public class Brand : EntityBase
    {
        public string? Name { get; set; }
        public byte[]? Icon { get; set; }

        public IEnumerable<Product> Products { get; set; }
    }
}
