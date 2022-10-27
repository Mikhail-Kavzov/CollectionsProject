namespace CollectionsProject.Repositories.Interfaces
{
    public interface IUserCollectionRepository<T>:IPagingRepository<T>
    {
        /// <summary>
        /// Get some items from collection
        /// </summary>
        /// <param name="itemsToSkip">Number of collection items to skip</param>
        /// <param name="itemsToTake">Number of collection items to take</param>
        /// <returns></returns>
        Task<IEnumerable<T>?> GetSomeItemsAsync(int itemsToSkip, int itemsToTake);
    }
}
