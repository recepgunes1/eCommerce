using eCommerce.Entity.ViewModels.User;
using eCommerce.Service.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Web.Areas.Profile.Controllers
{
    [Area("Profile")]
    [Authorize(Roles = "admin, customer")]
    public class HomeController : Controller
    {
        private readonly IUserService userService;
        private readonly ICommentService commentService;

        public HomeController(IUserService userService, ICommentService commentService)
        {
            this.userService = userService;
            this.commentService = commentService;
        }

        public async Task<IActionResult> Profile()
        {
            var user = await userService.GetAuthenticatedUserAsync<UpdateUserViewModel>();
            return View(user);
        }

        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangeUserPasswordViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await userService.UpdatePasswordAsync(viewModel);
                if (result.Succeeded)
                {
                    return View();
                }
            }
            return View();
        }

        public IActionResult Orders()
        {
            return View();
        }

        public async Task<IActionResult> Comments()
        {
            var user = await userService.GetAuthenticatedUserAsync<UserViewModel>();
            var comments = await commentService.GetAllCommentsToUserIdNonDeletedAsync(user.Id);
            return View(comments);
        }

        public async Task<IActionResult> DeleteComment(Guid id)
        {
            await commentService.DeleteCommentAsync(id);
            return RedirectToAction("Comments", "Profile", new { Area = "Profile" });
        }
    }
}
