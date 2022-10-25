using CollectionsProject.Models.UserModels;

namespace CollectionsProject.Repositories.Interfaces
{
    public interface IUserRepository : IPagingRepository<User>
    {
        Task<IEnumerable<User>> GetUsersAsync(string[] id);
        Task<int> CountUsersAsync();
    }
}
