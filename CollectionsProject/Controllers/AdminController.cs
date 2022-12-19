using CollectionsProject.Models.UserModels;
using CollectionsProject.Repositories.Interfaces;
using CollectionsProject.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CollectionsProject.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly UserManager<User> _userManager;

        public AdminController(IAdminService adminService, UserManager<User> userManager)
        {
            _userManager = userManager;
            _adminService = adminService;
        }

        public async Task<IActionResult> UserManager()
        {
            int count = await _adminService.CountUsersAsync();
            ViewBag.UserCount = _adminService.CountPagesInUsers(count);
            return View();
        }      

        [HttpGet]
        public async Task<IActionResult> UserPage(int Page = 0) //pagination
        {
            var users = await _adminService.GetSomeItemsAsync(Page);
            return PartialView(users);
        }

        private async Task<IActionResult> CheckCurrentStatusUser() //check current admin status after operation
        {
            var currentUser = await _userManager.GetUserAsync(User);
            bool hasNoAccess = currentUser == null || currentUser?.Status == Status.Blocked 
                || currentUser?.Role == Role.User;
            if (hasNoAccess)
                return Json(new { redirectToUrl = Url.Action("Logout", "Account") });
            return Json("");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUsers(string[] id)
        {
            await _adminService.DeleteUsers(id);
            return await CheckCurrentStatusUser();
        }

        [HttpPut]
        public async Task<IActionResult> UnBlockUsers(string[] id)
        {
            await _adminService.UnBlockUsers(id);
            return Json("");
        }

        [HttpPut]
        public async Task<IActionResult> BlockUsers(string[] id)
        {
            await _adminService.BlockUsers(id);
            return await CheckCurrentStatusUser();
        }      

        [HttpPut]
        public async Task<IActionResult> AddAdminRole(string[] id)
        {
            await _adminService.AddAdminRole(id);
            return await CheckCurrentStatusUser();
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveAdminRole(string[] id)
        {
            await _adminService.RemoveAdminRole(id);
            return await CheckCurrentStatusUser();
        }
    }
}
