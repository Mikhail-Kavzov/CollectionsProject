using CollectionsProject.Hash;
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

        public async Task<IActionResult> UserManager()
        {
            int count = await _userRepository.CountUsersAsync();
            ViewBag.UserCount=CountPagesInUsers(count);
            return View();
        }

        private static int CountPagesInUsers(int collectionCount)
        {
            if (collectionCount % usersCount == 0)
                return collectionCount / usersCount;
            return collectionCount / usersCount + 1;
        }

        [HttpPost]
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

        private async Task<IActionResult> CheckCurrentStatusUser()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            bool hasNoAccess = currentUser == null || currentUser?.Status == Status.Blocked || currentUser?.Role == Role.User;
            if (hasNoAccess)
                return Json(new { redirectToUrl = Url.Action("Logout", "Account") });
            return Json("");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUsers(string[] id)
        {
            var users = await _userRepository.GetUsersAsync(id);
            foreach (var user in users)
            {
                await _userManager.UpdateSecurityStampAsync(user);
                _userRepository.Delete(user);
            }
            await _userRepository.SaveChangesAsync();
            return await CheckCurrentStatusUser();
        }

        [HttpPut]
        public async Task<IActionResult> UnBlockUsers(string[] id)
        {
            var users = await _userRepository.GetUsersAsync(id);
            ChangeUserStatus(users, Status.Active);
            await _userRepository.SaveChangesAsync();
            return Json("");
        }

        [HttpPut]
        public async Task<IActionResult> BlockUsers(string[] id)
        {
            var users = await _userRepository.GetUsersAsync(id);
            ChangeUserStatus(users, Status.Blocked);
            foreach (var user in users)
                await _userManager.UpdateSecurityStampAsync(user);
            await _userRepository.SaveChangesAsync();
            return await CheckCurrentStatusUser();
        }

        private async Task AddRole(IEnumerable<User> users, Role role, string addRole)
        {
            foreach (var user in users)
            {
                user.Role = role;
                _userRepository.Update(user);
                await _userManager.AddToRoleAsync(user, addRole);
                await _userManager.UpdateSecurityStampAsync(user);
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
                await _userManager.UpdateSecurityStampAsync(user);
            }
            await _userRepository.SaveChangesAsync();
        }

        [HttpPut]
        public async Task<IActionResult> AddAdminRole(string[] id)
        {
            var users = await _userRepository.GetUsersAsync(id);
            await AddRole(users, Role.Admin, "Admin");
            return await CheckCurrentStatusUser();
        }

        [HttpPut]
        public async Task<IActionResult> RemoveAdminRole(string[] id)
        {
            var users = await _userRepository.GetUsersAsync(id);
            await RemoveRole(users, Role.User, "Admin");
            return await CheckCurrentStatusUser();
        }
    }
}
