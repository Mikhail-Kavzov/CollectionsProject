using CollectionsProject.Models;

namespace CollectionsProject.Repositories.Interfaces
{
    public interface IPagingRepository<T> : ICRUDRepository<T>
    {
        /// <summary>
        /// Get item by id
        /// </summary>
        /// <param name="id">id of item</param>
        /// <returns></returns>
        Task<T?> GetItemAsync(string id);
    }
}
