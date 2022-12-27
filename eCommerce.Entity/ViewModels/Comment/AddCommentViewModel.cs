using System.ComponentModel.DataAnnotations;

namespace eCommerce.Entity.ViewModels.Comment
{
    public class AddCommentViewModel
    {
        [Required]
        public string Content { get; set; }
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }
    }
}
