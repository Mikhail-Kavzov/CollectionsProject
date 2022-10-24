using CollectionsProject.Models.UserModels;

namespace CollectionsProject.Repositories
{
    public interface IUserRepository:IPagingRepository<User>
    {
       Task<IEnumerable<User>> GetUsersAsync(string[] id);
       Task<int> CountUsersAsync();
    }
}
