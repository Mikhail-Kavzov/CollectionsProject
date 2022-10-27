using CollectionsProject.Context;
using CollectionsProject.Models.UserModels;
using CollectionsProject.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CollectionsProject.Repositories.Implementation
{
    public class UserRepository : AbstractRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationContext appContext) : base(appContext)
        {
        }

        public async Task<int> CountUsersAsync()
        {
            return await db.Users.CountAsync();
        }

        public void Create(User item)
        {
            db.Users.Add(item);
        }

        public void Delete(User item)
        {
            db.Users.Remove(item);
        }

        public async Task<User?> GetItemAsync(string id)
        {
            return await db.Users.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<User>?> GetSomeItemsAsync(int itemsToSkip, int itemsToTake)
        {

            return await db.Users.OrderBy(u => u.Id).Skip(itemsToSkip).Take(itemsToTake).ToListAsync();
        }

        public async Task<IEnumerable<User>> GetUsersAsync(string[] id)
        {
            return await db.Users.Where(u => id.Contains(u.Id)).ToListAsync();
        }

        public void Update(User item)
        {
            db.Users.Update(item);
        }
    }
}
