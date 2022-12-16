using eCommerce.Entity.Entities;
using eCommerce.Entity.ViewModels.Product;

namespace eCommerce.Entity.ViewModels.Comment
{
    public class CommentViewModel
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public bool IsVisible { get; set; }
        public SimpleProductViewModel Product { get; set; }
        public User User { get; set; }
    }
}
