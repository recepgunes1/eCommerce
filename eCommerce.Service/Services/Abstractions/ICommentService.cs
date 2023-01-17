using eCommerce.Entity.ViewModels.Comment;

namespace eCommerce.Service.Services.Abstractions
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentViewModel>> GetAllCommentsNonDeletedAsync();
        Task<IEnumerable<CommentViewModel>> GetAllCommentsDeletedAsync();
        Task<IEnumerable<CommentViewModel>> GetAllCommentsToUserIdNonDeletedAsync(Guid id);
        Task AddCommentAsync(AddCommentViewModel viewModel);
        Task DeleteCommentAsync(Guid id);
        Task ChangeCommentVisibilityAsync(Guid id);
        Task RestoreCommentAsync(Guid id);
        Task<(int Deleted, int Visible, int Invisible)> CountCommentsAsync();
    }
}
