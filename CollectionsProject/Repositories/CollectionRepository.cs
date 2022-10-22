using CollectionsProject.Context;
using CollectionsProject.Models.CollectionModels;
using Microsoft.EntityFrameworkCore;

namespace CollectionsProject.Repositories
{
    public class CollectionRepository:ICollectionRepository
    {
        private readonly ApplicationContext db;

        public CollectionRepository(ApplicationContext appContext)
        {
            db=appContext;
        }

        public void AddFieldsRange(IEnumerable<AddCollectionField> fields)
        {
            db.AddCollectionFields.AddRange(fields);
        }

        public async Task<string?> CheckIdAsync(string id)
        {
            return await db.Collections.Select(c => c.CollectionId.ToString()).FirstOrDefaultAsync(guid => guid == id);
        }

        public void Create(Collection item)
        {
            db.Collections.Add(item);
        }

        public void Delete(Collection item)
        {
            db.Collections.Remove(item);
        }

        public async Task<Collection?> GetItemAsync(string id)
        {
            return await db.Collections.Include(c=>c.User).Include(c=>c.AddFields).FirstOrDefaultAsync(c => c.CollectionId.ToString() == id);
        }

        public async Task<Collection?> GetItemIncludeFieldsAsync(string id)
        {
            return await db.Collections.Include(c => c.AddFields).FirstOrDefaultAsync(c => c.CollectionId.ToString() == id);
        }

        public async Task<IEnumerable<Collection>?> GetSomeItemsAsync(int itemsToSkip, int itemsToTake)
        {
            return await db.Collections.Include(c => c.User).OrderBy(c => c.CollectionId).Skip(itemsToSkip).Take(itemsToTake).ToListAsync();
        }

        public async Task<IEnumerable<Collection>?> GetUserItemsAsync(int itemsToSkip, int itemsToTake,string id)
        {
            return await db.Collections.Include(c => c.User).Where(c=>c.UserId==id).OrderBy(c => c.CollectionId).Skip(itemsToSkip).Take(itemsToTake).ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await db.SaveChangesAsync();
        }

        public void Update(Collection item)
        {
            db.Collections.Update(item);
        }
    }
}
