using eCommerce.Entity.ViewModels.Product;
using eCommerce.Entity.ViewModels.User;

namespace eCommerce.Entity.ViewModels.Comment
{
    public class CommentViewModel
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public bool IsVisible { get; set; }
        public SimpleProductViewModel Product { get; set; }
        public SimpleUserViewModel User { get; set; }
    }
}
