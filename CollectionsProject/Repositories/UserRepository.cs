using CollectionsProject.Context;
using CollectionsProject.Models.UserModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CollectionsProject.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext db;

        public UserRepository(ApplicationContext appContext)
        {
            db= appContext;
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

        public Task<IEnumerable<User>?> GetUserItemsAsync(int itemsToSkip, int itemsToTake, string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<User>> GetUsersAsync(string[] id)
        {
            return await db.Users.Where(u => id.Contains(u.Id)).ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await db.SaveChangesAsync();
        }

        public void Update(User item)
        {
            db.Users.Update(item);
        }
    }
}
