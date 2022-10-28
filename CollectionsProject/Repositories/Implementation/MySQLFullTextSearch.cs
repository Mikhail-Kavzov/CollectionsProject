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

        public async Task<IEnumerable<SearchModel>> SearchInCollections(string searchString)
        {
            return await db.Collections.
                Where(c => EF.Functions.Match(new[] { c.Name, c.Description }, searchString, MySqlMatchSearchMode.NaturalLanguage) ||
                c.AddFields.Any(ai => EF.Functions.Match(ai.Name, searchString, MySqlMatchSearchMode.NaturalLanguage))).
                Select(c => new SearchModel()
                {
                    Name = c.Name,
                    Id = c.CollectionId.ToString(),
                }).ToListAsync();
        }

        public async Task<IEnumerable<SearchModel>> SearchInItems(string searchString)
        {
            return await db.Items.
                Where(i => EF.Functions.Match(i.Name, searchString, MySqlMatchSearchMode.NaturalLanguage) ||
                i.Comments.Any(c => EF.Functions.Match(c.CommentText, searchString, MySqlMatchSearchMode.NaturalLanguage)) ||
                i.Tags.Any(t => EF.Functions.Match(t.TagName, searchString, MySqlMatchSearchMode.NaturalLanguage))).
                Select(i => new SearchModel()
                {
                    Id = i.ItemId.ToString(),
                    Name = i.Name,
                }).ToListAsync();
        }
    }
}
