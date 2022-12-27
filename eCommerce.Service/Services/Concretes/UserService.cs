using AutoMapper;
using eCommerce.Entity.Entities;
using eCommerce.Entity.ViewModels.Auth;
using eCommerce.Entity.ViewModels.User;
using eCommerce.Service.Extensions;
using eCommerce.Service.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace eCommerce.Service.Services.Concretes
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;
        private readonly SignInManager<User> signInManager;
        private readonly ClaimsPrincipal _user;

        public UserService(IHttpContextAccessor httpContextAccessor, IMapper mapper, UserManager<User> userManager, RoleManager<Role> roleManager, SignInManager<User> signInManager)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.mapper = mapper;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
            this._user = this.httpContextAccessor.HttpContext.User;
        }

        public async Task<IdentityResult> AddUserAsync(AddUserViewModel viewModel)
        {
            var mappedUser = mapper.Map<User>(viewModel);
            mappedUser.UserName = mappedUser.Email;
            mappedUser.LockoutEnabled = false;
            var result = await userManager.CreateAsync(mappedUser, viewModel.Password);
            if (result.Succeeded)
            {
                var role = await roleManager.FindByIdAsync(viewModel.RoleId.ToString());
                await userManager.AddToRoleAsync(mappedUser, role.Name);
            }
            return result;
        }

        public async Task<IEnumerable<Role>> GetAllRolesAsync()
        {
            var roles = await roleManager.Roles.ToListAsync();
            return roles;
        }

        public async Task<IEnumerable<SimpleUserViewModel>> GetAllUsersWithRoleNonLockedOutAsync()
        {
            var users = await userManager.Users.Where(p => !p.LockoutEnabled).ToListAsync();
            var mappedUsers = mapper.Map<IEnumerable<SimpleUserViewModel>>(users);
            foreach (var mappedUser in mappedUsers)
            {
                var role = await userManager.GetRolesAsync(users.Where(u => u.Email == mappedUser.Email).First());
                mappedUser.Role = role.First();
            }
            return mappedUsers;
        }

        public async Task<T> GetAuthenticatedUserAsync<T>()
        {
            Guid userId = _user.GetLoggedInUserId();
            var user = await userManager.FindByIdAsync(userId.ToString());
            var mappedUser = mapper.Map<T>(user);
            return mappedUser;
        }

        public async Task<User> GetUserByGuidAsync(Guid id)
        {
            var user = await userManager.FindByIdAsync(id.ToString());
            return user;
        }

        public async Task<string> GetUserRoleAsync(User user)
        {
            return string.Join("", await userManager.GetRolesAsync(user));
        }

        public async Task<IdentityResult> UpdateUserAsync(UpdateUserViewModel viewModel)
        {
            var user = await GetUserByGuidAsync(viewModel.Id);
            var userRole = await GetUserRoleAsync(user);
            mapper.Map(viewModel, user);
            user.UserName = user.Email;
            if (viewModel.RoleId == Guid.Empty)
            {
                viewModel.RoleId = await roleManager.Roles.Where(p => p.Name == userRole).Select(p => p.Id).FirstAsync();
            }
            var result = await userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                await userManager.RemoveFromRoleAsync(user, userRole);
                var findRole = await roleManager.FindByIdAsync(viewModel.RoleId.ToString());
                await userManager.AddToRoleAsync(user, findRole.Name);
            }
            return result;
        }

        public async Task<(IdentityResult locukoutEnabled, IdentityResult lockoutEnd)> LockoutUserAsync(LockoutUserViewModel viewModel)
        {
            var user = await GetUserByGuidAsync(viewModel.Id);
            var lockoutEnabled = await userManager.SetLockoutEnabledAsync(user, viewModel.LockoutEnabled);
            var lockoutEnd = await userManager.SetLockoutEndDateAsync(user, viewModel.LockoutEnd);
            return (lockoutEnabled, lockoutEnd);
        }

        public async Task<(IdentityResult locukoutEnabled, IdentityResult lockoutEnd)> RestoreUserAsync(Guid id)
        {
            var user = await GetUserByGuidAsync(id);
            var lockoutEnabled = await userManager.SetLockoutEnabledAsync(user, false);
            var lockoutEnd = await userManager.SetLockoutEndDateAsync(user, null);
            return (lockoutEnabled, lockoutEnd);
        }

        public async Task<IEnumerable<UserViewModel>> GetAllUsersWithRoleLockedOutAsync()
        {

            var users = await userManager.Users.Where(p => p.LockoutEnabled).ToListAsync();
            var mappedUsers = mapper.Map<IEnumerable<UserViewModel>>(users);
            foreach (var mappedUser in mappedUsers)
            {
                var role = await userManager.GetRolesAsync(users.Where(u => u.Email == mappedUser.Email).First());
                mappedUser.Role = role.First();
            }
            return mappedUsers;
        }

        public async Task<IdentityResult> UpdatePasswordAsync(ChangeUserPasswordViewModel viewModel)
        {
            var user = await GetAuthenticatedUserAsync<User>();
            var check = await signInManager.CheckPasswordSignInAsync(user, viewModel.CurrentPassowrd, false);
            if (check.Succeeded)
            {
                user.SecurityStamp = Guid.NewGuid().ToString();
                var token = await userManager.GeneratePasswordResetTokenAsync(user);
                var result = await userManager.ResetPasswordAsync(user, token, viewModel.Password);
                return result;
            }
            return IdentityResult.Failed(new IdentityError() { Description = "Current password is wrong." });
        }

        public async Task<IdentityResult> SignUpAsCustomerAsync(SignUpViewModel viewModel)
        {
            var mappedUser = mapper.Map<User>(viewModel);
            mappedUser.UserName = mappedUser.Email;
            mappedUser.LockoutEnabled = false;
            var result = await userManager.CreateAsync(mappedUser, viewModel.Password);
            if (result.Succeeded)
            {
                var role = await roleManager.FindByNameAsync("customer");
                await userManager.AddToRoleAsync(mappedUser, role.Name);
            }
            return result;
        }
    }
}
