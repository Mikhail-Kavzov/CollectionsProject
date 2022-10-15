using CollectionsProject.Models.UserModels;

namespace CollectionsProject.Repositories
{
    public interface IUserRepository:IPagingRepository<User>
    {
        public Task<IEnumerable<User>> GetUsersAsync(string[] id);
    }
}
