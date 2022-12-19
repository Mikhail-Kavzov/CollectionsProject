using CollectionsProject.Models.UserModels;
using CollectionsProject.Repositories.Interfaces;
using CollectionsProject.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CollectionsProject.Services.Implementation
{
    public class AdminService : IAdminService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;
        private const int usersCount = 10;

        public AdminService(IUserRepository userRepository, UserManager<User> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }

        public int CountPagesInUsers(int collectionCount)
        {
            if (collectionCount % usersCount == 0)
                return collectionCount / usersCount;
            return collectionCount / usersCount + 1;
        }

        public async Task<int> CountUsersAsync()
        {
            return await _userRepository.CountUsersAsync();
        }

        public void ChangeUserStatus(IEnumerable<User> users, Status status)
        {
            foreach (var user in users)
            {
                user.Status = status;
                _userRepository.Update(user);
            }
        }

        public async Task<IEnumerable<User>?> GetSomeItemsAsync(int page)
        {
            return await _userRepository.GetSomeItemsAsync(page * usersCount, usersCount);
        }

        public async Task DeleteUsers(string[] id)
        {
            var users = await _userRepository.GetUsersAsync(id);
            foreach (var user in users)
            {
                await _userManager.UpdateSecurityStampAsync(user);
                _userRepository.Delete(user);
            }
            await _userRepository.SaveChangesAsync();
        }

        public async Task UnBlockUsers(string[] id)
        {
            var users = await _userRepository.GetUsersAsync(id);
            ChangeUserStatus(users, Status.Active);
            await _userRepository.SaveChangesAsync();
        }

        public async Task BlockUsers(string[] id)
        {
            var users = await _userRepository.GetUsersAsync(id);
            ChangeUserStatus(users, Status.Blocked);
            foreach (var user in users)
                await _userManager.UpdateSecurityStampAsync(user);
            await _userRepository.SaveChangesAsync();
        }

        public async Task AddAdminRole(string[] id)
        {
            var users = await _userRepository.GetUsersAsync(id);
            await AddRole(users, Role.Admin, "Admin");
        }

        public async Task RemoveAdminRole(string[] id)
        {
            var users = await _userRepository.GetUsersAsync(id);
            await RemoveRole(users, Role.User, "Admin");
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
    }
}
