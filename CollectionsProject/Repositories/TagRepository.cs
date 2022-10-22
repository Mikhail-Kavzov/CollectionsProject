using CollectionsProject.Context;
using CollectionsProject.Models.ItemModels;
using Microsoft.EntityFrameworkCore;

namespace CollectionsProject.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly ApplicationContext db;

        public TagRepository(ApplicationContext context)
        {
            db = context;
        }

        public void Create(Tag item)
        {
            db.Tags.Add(item);
        }

        public void Delete(Tag item)
        {
            db.Tags.Remove(item);
        }

        public async Task<IEnumerable<string>> GetTagNamesAsync()
        {
           return await db.Tags.Select(t => t.TagName).Distinct().ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await db.SaveChangesAsync();
        }

        public void Update(Tag item)
        {
            db.Tags.Update(item);
        }
    }
}
