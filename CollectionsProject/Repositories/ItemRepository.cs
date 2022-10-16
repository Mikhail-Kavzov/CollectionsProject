using CollectionsProject.Context;
using CollectionsProject.Models.ItemModels;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace CollectionsProject.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly ApplicationContext db;

        public ItemRepository(ApplicationContext context)
        {
            db = context;
        }

        public void AddFieldRange(IEnumerable<AddItemField> fields)
        {
            db.AddItemFields.AddRange(fields);
        }

        public void AddTagRange(IEnumerable<Tag> tags)
        {
            db.Tags.AddRange(tags);
        }

        public void Create(Item item)
        {
            db.Items.Add(item);
        }

        public void Delete(Item item)
        {
            db.Items.Remove(item);
        }

        public async Task<IEnumerable<Item>> GetAllAsync()
        {
            return await db.Items.ToListAsync();
        }

        public async Task<int> GetCountAsync()
        {
            return await db.Items.CountAsync();
        }

        public async Task<Item?> GetItemAsync(string id)
        {
            return await db.Items.Include(i => i.AddItems).ThenInclude(c => c.AddCollectionFields)
                .Include(i=>i.Collection).Include(i => i.Tags).Include(i=>i.Comments)
                .FirstOrDefaultAsync(i=>i.ItemId.ToString()==id);
        }

        public Task <IEnumerable<Item>?> GetSomeItemsAsync(int itemsToSkip, int itemsToTake)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Item>?> GetUserItemsAsync(int itemsToSkip, int itemsToTake, string id)
        {
            return await db.Items.Where(i => i.CollectionId.ToString() == id)
                .Include(i=>i.Collection.User).Include(i => i.AddItems).ThenInclude(ai=>ai.AddCollectionFields)
                .OrderBy(i => i.ItemId).Skip(itemsToSkip).Take(itemsToTake).ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await db.SaveChangesAsync();
        }

        public void Update(Item item)
        {
            db.Items.Update(item);
        }
    }
}
