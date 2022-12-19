using CollectionsProject.Models.UserModels;

namespace CollectionsProject.Services.Interfaces
{
    public interface IAdminService
    {
        int CountPagesInUsers(int collectionCount);
        Task<int> CountUsersAsync();
        void ChangeUserStatus(IEnumerable<User> users, Status status);
        Task<IEnumerable<User>?> GetSomeItemsAsync(int page);
        Task DeleteUsers(string[] id);
        Task UnBlockUsers(string[] id);
        Task BlockUsers(string[] id);
        Task AddAdminRole(string[] id);
        Task RemoveAdminRole(string[] id);
    }
}
