using AutoMapper;
using eCommerce.Entity.ViewModels.User;
using eCommerce.Service.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly IMapper mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            this.userService = userService;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var users = await userService.GetAllUsersWithRoleNonLockedOutAsync();
            return View(users);
        }

        public async Task<IActionResult> Add()
        {
            var viewModel = new AddUserViewModel();
            viewModel.Roles = await userService.GetAllRolesAsync();
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddUserViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await userService.AddUserAsync(viewModel);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "User", new { Area = "Admin" });
                }
            }

            return View(viewModel);
        }

        public async Task<IActionResult> Update(Guid id)
        {
            var user = await userService.GetUserByGuidAsync(id);
            var mappedUser = mapper.Map<UpdateUserViewModel>(user);
            mappedUser.Roles = await userService.GetAllRolesAsync();
            return View(mappedUser);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateUserViewModel viewModel)
        {
            var user = await userService.GetUserByGuidAsync(viewModel.Id);
            if (user != null)
            {
                var roles = await userService.GetAllRolesAsync();
                if (ModelState.IsValid)
                {
                    var map = mapper.Map(viewModel, user);
                    user.UserName = viewModel.Email;
                    user.SecurityStamp = Guid.NewGuid().ToString();
                    var result = await userService.UpdateUserAsync(viewModel);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "User", new { Area = "Admin" });
                    }
                    else
                    {
                        return View(new UpdateUserViewModel() { Roles = roles });
                    }
                }
                else
                {
                    return View(new UpdateUserViewModel() { Roles = roles });
                }
            }
            return NotFound();
        }

        public async Task<IActionResult> Lockout(Guid id)
        {
            var user = await userService.GetUserByGuidAsync(id);
            var mappedUser = mapper.Map<LockoutUserViewModel>(user);
            return View(mappedUser);
        }

        [HttpPost]
        public async Task<IActionResult> Lockout(LockoutUserViewModel viewModel)
        {
            var user = await userService.GetUserByGuidAsync(viewModel.Id);
            if (user != null)
            {
                if (ModelState.IsValid)
                {
                    var result = await userService.LockoutUserAsync(viewModel);
                    if (result.locukoutEnabled.Succeeded && result.lockoutEnd.Succeeded)
                    {
                        return RedirectToAction("Index", "User", new { Area = "Admin" });
                    }
                    else
                    {
                        return View(viewModel);
                    }
                }
                else
                {
                    return View(viewModel);
                }
            }
            return NotFound();
        }


    }
}
