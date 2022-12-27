using eCommerce.Entity.ViewModels.Brand;
using eCommerce.Entity.ViewModels.Category;
using eCommerce.Entity.ViewModels.Comment;
using eCommerce.Entity.ViewModels.Image;

namespace eCommerce.Entity.ViewModels.Product
{
    public class ProductWithCommentsViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public SimpleBrandViewModel Brand { get; set; }
        public SimpleCategoryViewModel Category { get; set; }
        public AddCommentViewModel AddComment { get; set; }
        public IEnumerable<CommentViewModel> Comments { get; set; }
        public IEnumerable<ImageViewModel> Images { get; set; }
    }
}
