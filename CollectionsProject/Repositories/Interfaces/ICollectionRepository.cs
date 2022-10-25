using CollectionsProject.Models.CollectionModels;

namespace CollectionsProject.Repositories.Interfaces
{
    public interface ICollectionRepository : IPagingRepository<Collection>
    {
        void AddFieldsRange(IEnumerable<AddCollectionField> fields);
        Task<string?> CheckIdAsync(string id);
        Task<Collection?> GetItemIncludeFieldsAsync(string id);
        Task<IEnumerable<Collection>> GetLargestCollections(int count);

    }
}
