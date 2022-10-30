using CollectionsProject.Models.ItemModels;

namespace CollectionsProject.Repositories.Interfaces
{
    public interface ITagRepository : IRepository<Tag>
    {   /// <summary>
        /// Get tag names for item
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<string>> GetTagNamesAsync();
        /// <summary>
        /// Get tags for tag cloud
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        Task<IEnumerable<string>> GetTagList(int count);
        /// <summary>
        /// Items that contain corresponding tag
        /// </summary>
        /// <param name="tagName"></param>
        /// <param name="itemsToSkip"></param>
        /// <param name="itemsToTake"></param>
        /// <returns></returns>
        Task<IEnumerable<Item>> GetTagItems(string tagName, int itemsToSkip, int itemsToTake);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tagName"></param>
        /// <returns>Number of tags in items</returns>
        Task<int>CountTagInItemsAsync(string tagName);
    }
}
