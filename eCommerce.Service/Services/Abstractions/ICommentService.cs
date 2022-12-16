using eCommerce.Entity.ViewModels.Comment;

namespace eCommerce.Service.Services.Abstractions
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentViewModel>> GetAllCommentsNonDeletedAsync();
        Task<IEnumerable<CommentViewModel>> GetAllCommentsDeletedAsync();
        Task DeleteCommentAsync(Guid id);
        Task BlockCommentAsync(Guid id);
        Task RestoreCommentAsync(Guid id);

    }
}
