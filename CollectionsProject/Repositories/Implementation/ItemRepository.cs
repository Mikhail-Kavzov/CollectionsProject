using CollectionsProject.Context;
using CollectionsProject.Models.CollectionModels;
using CollectionsProject.Models.ItemModels;
using CollectionsProject.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;

namespace CollectionsProject.Repositories.Implementation
{
    public class ItemRepository : AbstractRepository<Item>, IItemRepository
    {
        public ItemRepository(ApplicationContext context) : base(context)
        {
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

        //get item by id
        public async Task<Item?> GetItemAsync(string id)
        {
            return await db.Items.Include(i => i.AddItems).ThenInclude(c => c.AddCollectionFields)
                .Include(i => i.Collection).ThenInclude(c=>c.User).Include(i => i.Tags).Include(i => i.Comments)
                .FirstOrDefaultAsync(i => i.ItemId.ToString() == id);
        }
        //count items in collection (for main page)
        public async Task<int> GetItemCountAsync(string collectionId)
        {
            return await db.Items.Where(i => i.CollectionId.ToString() == collectionId).CountAsync();
        }

        //fiter
        public async Task<IEnumerable<Item>?> Filter(int itemsToSkip, int itemsToTake, string collectionId, string searchString = "")
        {
            IQueryable<Item> query = db.Items.Where(i => i.CollectionId.ToString() == collectionId)
                .Include(i => i.Collection.User).Include(i => i.AddItems).ThenInclude(ai => ai.AddCollectionFields);

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(i => i.Name.ToUpper().Contains(searchString.ToUpper()) ||
                i.AddItems.Any(ai => ai.Value.ToUpper().Contains(searchString.ToUpper()) &&
                (ai.AddCollectionFields.Type == CollectionFieldType.dateField ||
                ai.AddCollectionFields.Type == CollectionFieldType.stringField)));
            }
            return await query.OrderBy(i => i.Name).Skip(itemsToSkip).Take(itemsToTake).ToListAsync();
        }

        public void Update(Item item)
        {
            db.Items.Update(item);
        }

        //last items (for main page)
        public async Task<IEnumerable<Item>> GetLastItemsAsync(int count)
        {
            return await db.Items.Include(i => i.Collection).ThenInclude(c => c.User).OrderByDescending(i => i.CreatedDate).Take(count).ToListAsync();
        }
    }
}
