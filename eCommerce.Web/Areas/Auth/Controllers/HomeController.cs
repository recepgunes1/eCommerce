using eCommerce.Entity.Entities;
using eCommerce.Entity.ViewModels.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Web.Areas.Auth.Controllers
{
    [Area("Auth")]
    public class HomeController : Controller
    {
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;

        public HomeController(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(SignInViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(viewModel.EMail);
                if (user != null)
                {
                    var result = await signInManager.PasswordSignInAsync(user, viewModel.Password, viewModel.RememberMe, true);
                    if (result.Succeeded)
                    {
                        var roles = await userManager.GetRolesAsync(user);
                        if (roles.Contains("admin"))
                        {
                            return RedirectToAction("Index", "Home", new { Area = "" });
                        }
                    }
                }
            }
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(SignUpViewModel viewModel)
        {
            return View();
        }

        [Authorize]
        public IActionResult Logout()
        {
            return RedirectToAction("Index", "Home", new { Area = "" });
        }

        public IActionResult AccessDenied()
        {
            return RedirectToAction("Index");
        }
    }
}
