using CollectionsProject.Models.ItemModels;
using Microsoft.EntityFrameworkCore;

namespace CollectionsProject.Repositories.Interfaces
{
    public interface IFullTextSearch
    {   /// <summary>
        /// Find collections that contains search string in field
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        Task<IEnumerable<SearchModel>> SearchInCollections(string searchString);
        /// <summary>
        /// Find items that contains SearchString in fields
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        Task<IEnumerable<SearchModel>> SearchInItems(string searchString);
    }
}
