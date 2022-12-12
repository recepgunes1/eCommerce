using eCommerce.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eCommerce.Data.Mappings
{
    public class BrandMapping : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasIndex(p => p.Name).IsUnique().HasName("NameIndex");
        }
    }
}
