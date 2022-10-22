using CollectionsProject.Models.ItemModels;

namespace CollectionsProject.Repositories
{
    public interface IItemRepository : IPagingRepository<Item>
    {
        void AddFieldRange(IEnumerable<AddItemField> fields);
        void AddTagRange(IEnumerable<Tag> tags);
        Task<int> GetItemCountAsync(string collectionId);
    }
}
