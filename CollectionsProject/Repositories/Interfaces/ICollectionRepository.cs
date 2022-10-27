using CollectionsProject.Models.CollectionModels;

namespace CollectionsProject.Repositories.Interfaces
{
    public interface ICollectionRepository : IUserCollectionRepository<Collection>
    {
        /// <summary>
        /// add additional fields to collection
        /// </summary>
        /// <param name="fields">AddCollectionField class</param>
        void AddFieldsRange(IEnumerable<AddCollectionField> fields);
        /// <summary>
        /// Get Collection include its fields properties without values
        /// </summary>
        /// <param name="id">Collection id</param>
        /// <returns>Collection or null if no match</returns>
        Task<Collection?> GetItemIncludeFieldsAsync(string id);
        /// <summary>
        /// Get the largest collections from db
        /// </summary>
        /// <param name="count">Collections count to get</param>
        /// <returns>List of collections</returns>
        Task<IEnumerable<Collection>> GetLargestCollections(int count);
        /// <summary>
        /// Get collections corresponding to the user
        /// </summary>
        /// <param name="itemsToSkip">Number of items to skip</param>
        /// <param name="itemsToTake">Number of items to take</param>
        /// <param name="id">User id</param>
        /// <returns>List of collections</returns>
        Task<IEnumerable<Collection>?> GetUserItemsAsync(int itemsToSkip, int itemsToTake, string id);
    }
}
