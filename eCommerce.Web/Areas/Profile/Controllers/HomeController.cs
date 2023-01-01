using eCommerce.Entity.ViewModels.User;
using eCommerce.Service.Extensions;
using eCommerce.Service.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Web.Areas.Profile.Controllers
{
    [Area("Profile")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IUserService userService;
        private readonly ICommentService commentService;
        private readonly IShoppingSessionService shoppingSessionService;

        public HomeController(IUserService userService, ICommentService commentService, IShoppingSessionService shoppingSessionService)
        {
            this.userService = userService;
            this.commentService = commentService;
            this.shoppingSessionService = shoppingSessionService;
        }

        public async Task<IActionResult> Profile()
        {
            var user = await userService.GetAuthenticatedUserAsync<UpdateUserViewModel>();
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Profile(UpdateUserViewModel viewModel)
        {
            var authUser = await userService.GetAuthenticatedUserAsync<UpdateUserViewModel>();
            viewModel.Id = authUser.Id;
            var result = await userService.UpdateUserAsync(viewModel);
            result.AddToIdentityModelState(ModelState);
            return View(viewModel);
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
                else
                {
                    result.AddToIdentityModelState(ModelState);
                }
            }
            return View();
        }

        public IActionResult Orders()
        {
            return View();
        }

        public async Task<IActionResult> GetOrders()
        {
            var user = await userService.GetAuthenticatedUserAsync<SimpleUserViewModel>();
            var orders = await shoppingSessionService.GetOldOrdersAsync(user.Id);
            return Json(orders.Select(p => new { p.Id, p.CreatedDate, products = p.Products.Select(i => new { name = i.Name, brand = i.Brand.Name, i.Price }), total = p.Products.Sum(p => p.Price) }));
        }

        public IActionResult Comments()
        {
            return View();
        }

        public async Task<IActionResult> GetComments()
        {
            var user = await userService.GetAuthenticatedUserAsync<UserViewModel>();
            var comments = await commentService.GetAllCommentsToUserIdNonDeletedAsync(user.Id);
            return Json(comments.Select(p => new { p.Id, p.Content, p.IsVisible, product = p.Product.Name }));
        }

        public async Task<IActionResult> DeleteComment(Guid id)
        {
            await commentService.DeleteCommentAsync(id);
            return RedirectToAction("Comments", "Profile", new { Area = "Profile" });
        }
    }
}
