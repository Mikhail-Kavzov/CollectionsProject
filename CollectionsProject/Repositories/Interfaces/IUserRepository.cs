using CollectionsProject.Models.UserModels;

namespace CollectionsProject.Repositories.Interfaces
{
    public interface IUserRepository : IUserCollectionRepository<User>
    {
        Task<IEnumerable<User>> GetUsersAsync(string[] id);
        Task<int> CountUsersAsync();
    }
}
