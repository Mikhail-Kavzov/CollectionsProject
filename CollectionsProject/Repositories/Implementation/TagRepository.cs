using CollectionsProject.Context;
using CollectionsProject.Models.ItemModels;
using CollectionsProject.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CollectionsProject.Repositories.Implementation
{
    public class TagRepository : AbstractRepository<Tag>, ITagRepository
    {

        public TagRepository(ApplicationContext context) : base(context)
        {
        }

        public async Task<int> CountTagInItemsAsync(string tagName)
        {
            return await db.Tags.Where(t => t.TagName == tagName && t.Items.Count > 0).CountAsync();
        }

        public async Task<IEnumerable<Item>> GetTagItems(string tagName, int itemsToSkip, int itemsToTake)
        {
            return await db.Items.Include(i => i.Collection).ThenInclude(c => c.User)
                .Where(i => i.Tags.Any(t => t.TagName == tagName)).OrderBy(i => i.ItemId)
                .Skip(itemsToSkip).Take(itemsToTake).ToListAsync();
        }

        public async Task<IEnumerable<string>> GetTagList(int count)
        {
            return await db.Tags.Where(t => t.Items.Count > 0).OrderByDescending(t => t.Items.Count)
                .Take(count).Select(t => t.TagName).Distinct().ToListAsync();
        }

        public async Task<IEnumerable<string>> GetTagNamesAsync()
        {
            return await db.Tags.Select(t => t.TagName).Distinct().ToListAsync();
        }
    }
}
