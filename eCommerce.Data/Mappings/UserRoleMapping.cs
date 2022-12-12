using eCommerce.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eCommerce.Data.Mappings
{
    public class UserRoleMapping : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasKey(r => new { r.UserId, r.RoleId });

            builder.ToTable("UserRoles");

            builder.HasData(new UserRole()
            {
                UserId = Guid.Parse("71a153a8-6da3-4bec-8538-7ea03e273eae"),
                RoleId = Guid.Parse("5d9fb419-99c9-4d2a-9f22-4b95f70a6861")
            });
        }
    }
}
