using CollectionsProject.Models.CollectionModels;

namespace CollectionsProject.Repositories
{
    public interface ICollectionRepository:IPagingRepository<Collection>
    {
        public void AddFieldsRange(IEnumerable<AddCollectionField> fields);
        public Task<string?> CheckIdAsync(string id);
        public Task<Collection?> GetItemIncludeFieldsAsync(string id);
    }
}
