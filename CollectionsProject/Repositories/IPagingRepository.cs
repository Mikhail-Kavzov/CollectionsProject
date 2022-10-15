using CollectionsProject.Models;

namespace CollectionsProject.Repositories
{
    public interface IPagingRepository<T>:IRepository<T>
    {
        public Task<IEnumerable<T>?> GetSomeItemsAsync(int itemsToSkip, int itemsToTake);
        public Task<IEnumerable<T>?> GetUserItemsAsync(int itemsToSkip, int itemsToTake, string id);
        public Task<T?> GetItemAsync(string id);
    }
}
