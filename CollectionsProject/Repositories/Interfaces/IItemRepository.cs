using CollectionsProject.Models.ItemModels;

namespace CollectionsProject.Repositories.Interfaces
{
    public interface IItemRepository : IPagingRepository<Item>
    {
        void AddFieldRange(IEnumerable<AddItemField> fields);
        void AddTagRange(IEnumerable<Tag> tags);
        Task<int> GetItemCountAsync(string collectionId);
        Task<IEnumerable<Item>?> Filter(int itemsToSkip, int itemsToTake, string collectionId, string searchString = "");
        Task<IEnumerable<Item>> GetLastItemsAsync(int count);
    }
}
