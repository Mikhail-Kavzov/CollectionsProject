using CollectionsProject.Models.ItemModels;

namespace CollectionsProject.Repositories
{
    public interface ITagRepository:IRepository<Tag>
    {
        Task<IEnumerable<string>> GetTagNamesAsync();
        Task<IEnumerable<string>> GetTagList(int count);
        Task<IEnumerable<Item>> GetTagItems(string tagName, int itemsToSkip, int itemsToTake);
    }
}
