using eCommerce.Service.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class CommentController : Controller
    {
        private readonly ICommentService commentService;

        public CommentController(ICommentService commentService)
        {
            this.commentService = commentService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetComments()
        {
            var comments = await commentService.GetAllCommentsNonDeletedAsync();
            return Json(comments.Select(p => new { p.Id, p.Content, product = p.Product.Name, user = $"{p.User.FirstName} {p.User.LastName}", p.IsVisible, p.CreatedDate }));
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            await commentService.DeleteCommentAsync(id);
            return RedirectToAction("Index", "Comment", new { Area = "Admin" });
        }

        public async Task<IActionResult> Block(Guid id)
        {
            await commentService.BlockCommentAsync(id);
            return RedirectToAction("Index", "Comment", new { Area = "Admin" });
        }
    }
}
