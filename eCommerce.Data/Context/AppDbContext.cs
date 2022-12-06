using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Data.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ShoppingSession> ShoppingSessions { get; set; }
        public DbSet<User> Users { get; set; }

        public AppDbContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=test;Trusted_Connection=True;Trust Server Certificate=true;");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
