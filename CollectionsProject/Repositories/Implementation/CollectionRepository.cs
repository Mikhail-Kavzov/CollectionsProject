using CollectionsProject.Context;
using CollectionsProject.Models.CollectionModels;
using CollectionsProject.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CollectionsProject.Repositories.Implementation
{
    public class CollectionRepository : AbstractRepository<Collection>, ICollectionRepository
    {
        public CollectionRepository(ApplicationContext appContext) : base(appContext)
        {
        }

        public void AddFieldsRange(IEnumerable<AddCollectionField> fields)
        {
            db.AddCollectionFields.AddRange(fields);
        }

        public void Create(Collection item)
        {
            db.Collections.Add(item);
        }

        public void Delete(Collection item)
        {
            db.Collections.Remove(item);
        }

        //Get collection include User and additional Fields properties without values
        public async Task<Collection?> GetItemAsync(string id)
        {
            return await db.Collections.Include(c => c.User).Include(c => c.AddFields).FirstOrDefaultAsync(c => c.CollectionId.ToString() == id);
        }

        //Get collection include additional Fields without values
        public async Task<Collection?> GetItemIncludeFieldsAsync(string id)
        {
            return await db.Collections.Include(c=>c.User).Include(c => c.AddFields).FirstOrDefaultAsync(c => c.CollectionId.ToString() == id);
        }

        //The largest collections (for main page)
        public async Task<IEnumerable<Collection>> GetLargestCollections(int count)
        {
            return await db.Collections.Include(c => c.User).OrderByDescending(c => c.Items.Count).Take(count).ToListAsync();
        }

        //pagination for collection page
        public async Task<IEnumerable<Collection>?> GetSomeItemsAsync(int itemsToSkip, int itemsToTake)
        {
            return await db.Collections.Include(c => c.User).OrderBy(c => c.CollectionId).Skip(itemsToSkip).Take(itemsToTake).ToListAsync();
        }

        //pagination for personal user page
        public async Task<IEnumerable<Collection>?> GetUserItemsAsync(int itemsToSkip, int itemsToTake, string id)
        {
            return await db.Collections.Include(c => c.User).Where(c => c.UserId == id).OrderBy(c => c.CollectionId).Skip(itemsToSkip).Take(itemsToTake).ToListAsync();
        }

        public void Update(Collection item)
        {
            db.Collections.Update(item);
        }
    }
}
