using eCommerce.Entity.Entities;
using eCommerce.Entity.ViewModels.User;
using Microsoft.AspNetCore.Identity;

namespace eCommerce.Service.Services.Abstractions
{
    public interface IUserService
    {
        Task<IEnumerable<SimpleUserViewModel>> GetAllUsersWithRoleNonLockedOutAsync();
        Task<IEnumerable<UserViewModel>> GetAllUsersWithRoleLockedOutAsync();
        Task<IEnumerable<Role>> GetAllRolesAsync();
        Task<IdentityResult> AddUserAsync(AddUserViewModel viewModel);
        Task<User> GetUserByGuidAsync(Guid id);
        Task<IdentityResult> UpdateUserAsync(UpdateUserViewModel viewModel);
        Task<string> GetUserRoleAsync(User user);
        Task<(IdentityResult locukoutEnabled, IdentityResult lockoutEnd)> LockoutUserAsync(LockoutUserViewModel viewModel);
        Task<(IdentityResult locukoutEnabled, IdentityResult lockoutEnd)> RestoreUserAsync(Guid id);
        Task<T> GetAuthenticatedUserAsync<T>();
        Task<IdentityResult> UpdatePasswordAsync(ChangeUserPasswordViewModel viewModel);
    }
}
