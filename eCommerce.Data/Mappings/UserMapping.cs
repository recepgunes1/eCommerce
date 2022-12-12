using eCommerce.Entity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eCommerce.Data.Mappings
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {

            builder.HasIndex(u => u.NormalizedEmail).HasName("EmailIndex");

            builder.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();

            builder.Property(u => u.Email).HasMaxLength(256);
            builder.Property(u => u.NormalizedEmail).HasMaxLength(256);

            builder.Ignore(p => p.UserName);
            builder.Ignore(p => p.NormalizedUserName);
            builder.Ignore(p => p.EmailConfirmed);
            builder.Ignore(p => p.PhoneNumber);
            builder.Ignore(p => p.PhoneNumberConfirmed);
            builder.Ignore(p => p.TwoFactorEnabled);

            builder.ToTable("Users");

            builder.HasMany<UserClaim>().WithOne().HasForeignKey(uc => uc.UserId).IsRequired();

            builder.HasMany<UserLogin>().WithOne().HasForeignKey(ul => ul.UserId).IsRequired();

            builder.HasMany<UserToken>().WithOne().HasForeignKey(ut => ut.UserId).IsRequired();

            builder.HasMany<UserRole>().WithOne().HasForeignKey(ur => ur.UserId).IsRequired();

            var admin = new User()
            {
                Id = Guid.Parse("71a153a8-6da3-4bec-8538-7ea03e273eae"),
                FirstName = "admin",
                LastName = "admin",
                DateBirth = new DateTime(2000, 10, 23),
                Address = "çermik",
                Email = "admin@system.com",
                NormalizedEmail = "ADMIN@SYSTEM.COM",
                SecurityStamp = Guid.NewGuid().ToString()
            };
            admin.PasswordHash = CreatePasswordHash(admin, "123456");

            builder.HasData(admin);
        }
        private string CreatePasswordHash(User user, string password)
        {
            var hasher = new PasswordHasher<User>();
            return hasher.HashPassword(user, password);
        }
    }
}
