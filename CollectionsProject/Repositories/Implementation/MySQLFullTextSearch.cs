using CollectionsProject.Context;
using CollectionsProject.Models.ItemModels;
using CollectionsProject.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CollectionsProject.Repositories.Implementation
{
    public class MySQLFullTextSearch : IFullTextSearch
    {
        private readonly ApplicationContext db;

        public MySQLFullTextSearch(ApplicationContext db)
        {
            this.db = db;
        }


        public async Task<IEnumerable<SearchModel>> SearchInCollections(string searchString, MySqlMatchSearchMode mode = MySqlMatchSearchMode.Boolean)
        {
            return await db.Collections.
                Where(c => EF.Functions.Match(new[] { c.Name, c.Description }, searchString, mode) ||
                c.AddFields.Any(ai => EF.Functions.Match(ai.Name, searchString, mode))).
                Select(c => new SearchModel()
                {
                    Name = c.Name,
                    Id = c.CollectionId.ToString(),
                }).ToListAsync();
        }

        public async Task<IEnumerable<SearchModel>> SearchInItems(string searchString, MySqlMatchSearchMode mode = MySqlMatchSearchMode.Boolean)
        {
            return await db.Items.
                Where(i => EF.Functions.Match(i.Name, searchString, mode) ||
                i.AddItems.Any(ai => EF.Functions.Match(ai.Value, searchString, mode)) ||
                i.Comments.Any(c => EF.Functions.Match(c.CommentText, searchString, mode)) ||
                i.Tags.Any(t => EF.Functions.Match(t.TagName, searchString, mode))).
                Select(i => new SearchModel()
                {
                    Id = i.ItemId.ToString(),
                    Name = i.Name,
                }).ToListAsync();
        }
    }
}
