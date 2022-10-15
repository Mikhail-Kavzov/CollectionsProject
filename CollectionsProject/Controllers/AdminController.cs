using CollectionsProject.Models.UserModels;
using CollectionsProject.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CollectionsProject.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;
        private const int usersCount = 10;

        public AdminController(IUserRepository userRepository, UserManager<User> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }

        public IActionResult UserManager() => View();

        [HttpGet]
        public async Task<IActionResult> UserPage(int Page = 0)
        {
            var users = await _userRepository.GetSomeItemsAsync(Page * usersCount, usersCount);
            return PartialView(users);
        }

        private void ChangeUserStatus(IEnumerable<User> users, Status status)
        {
            foreach (var user in users)
            {
                user.Status = status;
                _userRepository.Update(user);
            }
        }

        private async Task<bool> CheckCurrentStatusUser()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            return currentUser == null || currentUser?.Status == Status.Blocked || currentUser?.Role == Role.User;
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUsers(string[] id)
        {
            if (await CheckCurrentStatusUser())
                return Json(new { redirectToUrl = Url.Action("Logout", "Account") });
            var users = await _userRepository.GetUsersAsync(id);
            foreach (var user in users)
            {
                _userRepository.Delete(user);
            }
            await _userRepository.SaveChangesAsync();
            if (await CheckCurrentStatusUser())
                return Json(new { redirectToUrl = Url.Action("Logout", "Account") });
            return Json("");
        }

        [HttpPut]
        public async Task<IActionResult> UnBlockUsers(string[] id)
        {
            if (await CheckCurrentStatusUser())
                return Json(new { redirectToUrl = Url.Action("Logout", "Account") });
            var users = await _userRepository.GetUsersAsync(id);
            ChangeUserStatus(users, Status.Active);
            await _userRepository.SaveChangesAsync();
            return Json("");
        }

        [HttpPut]
        public async Task<IActionResult> BlockUsers(string[] id)
        {
            if (await CheckCurrentStatusUser())
                return Json(new { redirectToUrl = Url.Action("Logout", "Account") });
            var users = await _userRepository.GetUsersAsync(id);
            ChangeUserStatus(users, Status.Blocked);
            await _userRepository.SaveChangesAsync();
            if (await CheckCurrentStatusUser())
                return Json(new { redirectToUrl = Url.Action("Logout", "Account") });
            return Json("");
        }

        private async Task AddRole(IEnumerable<User> users, Role role, string addRole)
        {
            foreach (var user in users)
            {
                user.Role = role;
                _userRepository.Update(user);
                await _userManager.AddToRoleAsync(user, addRole);
            }
            await _userRepository.SaveChangesAsync();
        }

        private async Task RemoveRole(IEnumerable<User> users, Role role, string removeRole)
        {
            foreach (var user in users)
            {
                user.Role = role;
                _userRepository.Update(user);
                await _userManager.RemoveFromRoleAsync(user, removeRole);
            }
            await _userRepository.SaveChangesAsync();
        }

        [HttpPut]
        public async Task<IActionResult> AddAdminRole(string[] id)
        {
            if (await CheckCurrentStatusUser())
                return Json(new { redirectToUrl = Url.Action("Logout", "Account") });
            var users = await _userRepository.GetUsersAsync(id);
            await AddRole(users, Role.Admin, "Admin");
            if (await CheckCurrentStatusUser())
                return Json(new { redirectToUrl = Url.Action("Logout", "Account") });
            return Json("");
        }

        [HttpPut]
        public async Task<IActionResult> RemoveAdminRole(string[] id)
        {
            if (await CheckCurrentStatusUser())
                return Json(new { redirectToUrl = Url.Action("Logout", "Account") });
            var users = await _userRepository.GetUsersAsync(id);
            await RemoveRole(users, Role.User, "Admin");
            if (await CheckCurrentStatusUser())
                return Json(new { redirectToUrl = Url.Action("Logout", "Account") });
            return Json("");
        }
    }
}
