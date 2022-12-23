using AutoMapper;
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
        private readonly IMapper mapper;

        public HomeController(SignInManager<User> signInManager, UserManager<User> userManager, IMapper mapper)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.mapper = mapper;
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
                        return View();
                    }
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home", new { Area = "" });
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
            if (ModelState.IsValid)
            {
                var user = mapper.Map<User>(viewModel);
                System.Diagnostics.Debug.Print("");
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
