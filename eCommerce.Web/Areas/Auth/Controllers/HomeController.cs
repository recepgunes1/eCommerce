using eCommerce.Entity.Entities;
using eCommerce.Entity.ViewModels.Auth;
using eCommerce.Service.Extensions;
using eCommerce.Service.Services.Abstractions;
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
        private readonly IUserService userService;

        public HomeController(SignInManager<User> signInManager, UserManager<User> userManager, IUserService userService)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.userService = userService;
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
                var user = await userManager.FindByEmailAsync(viewModel.Email);
                if (user != null)
                {
                    var result = await signInManager.PasswordSignInAsync(user, viewModel.Password, viewModel.RememberMe, true);
                    if (result.IsLockedOut)
                    {
                        ModelState.AddModelError("", "Account has been locked out.");
                        return View();
                    }
                    else if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home", new { Area = "" });
                    }
                    else
                    {
                        ModelState.AddModelError("", result.ToString());
                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError("", "E-Mail wasn't registered the system.");
                    return View();
                }
            }
            else
            {
                ModelState.AddModelError("", "Your informations ain't been confirmed.");
                return View();
            }
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(SignUpViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await userService.SignUpAsCustomerAsync(viewModel);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home", new { Area = "" });
                }
                result.AddToIdentityModelState(ModelState);
            }
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home", new { Area = "" });
        }

        public IActionResult AccessDenied()
        {
            return RedirectToAction("Index");
        }
    }
}
