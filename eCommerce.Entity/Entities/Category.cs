using eCommerce.Core.Entities;

namespace eCommerce.Entity.Entities
{
    public class Category : EntityBase
    {
        public string? Name { get; set; }

        public Guid? ParentCategoryId { get; set; }
        public Category? ParentCategory { get; set; }

        public IEnumerable<Product> Products { get; set; }
    }
}
