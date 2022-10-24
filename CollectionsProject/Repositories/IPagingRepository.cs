using CollectionsProject.Models;

namespace CollectionsProject.Repositories
{
    public interface IPagingRepository<T>:IRepository<T>
    {
        Task<IEnumerable<T>?> GetSomeItemsAsync(int itemsToSkip, int itemsToTake);
        Task<IEnumerable<T>?> GetUserItemsAsync(int itemsToSkip, int itemsToTake, string id);
        Task<T?> GetItemAsync(string id);
    }
}
