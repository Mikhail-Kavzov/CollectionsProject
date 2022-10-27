using CollectionsProject.Models;

namespace CollectionsProject.Repositories.Interfaces
{
    public interface IPagingRepository<T> : ICRUDRepository<T>
    {
        Task<T?> GetItemAsync(string id);
    }
}
