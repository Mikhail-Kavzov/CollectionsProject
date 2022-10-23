using CollectionsProject.Models.CollectionModels;
using CollectionsProject.Models.ItemModels;
using CollectionsProject.Repositories;
using CollectionsProject.Services.Interfaces;
using CollectionsProject.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace CollectionsProject.Services.Implementation
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;

        public ItemService(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public AddItemField CreateAddField(string value, AddCollectionField collectionField, Item item)
        {
            AddItemField field = new()
            {
                Value = value,
                Item = item,
                AddCollectionFields = collectionField,
            };
            return field;
        }

        public List<AddItemField> CreateFields(List<FieldViewModel> model, List<AddCollectionField> colField, Item item)
        {
            List<AddItemField> addItemFields = new();
            for (int i = 0; i < model.Count; i++)
            {
                if (model[i].CustomFieldViewModel.FieldType == CollectionFieldType.dateField)
                {
                    model[i].Value = DateTime.Parse(model[i].Value).ToShortDateString();
                }
                addItemFields.Add(CreateAddField(model[i].Value, colField[i], item));
            }
            return addItemFields;
        }

        public async Task<Item> CreateNewItem(ItemViewModel model, Collection collection)
        {
            var tags = CreateTags(model.Tags);
            _itemRepository.AddTagRange(tags);
            Item item = new()
            {
                Name = model.Name,
                Collection = collection,
                Tags = tags,
                CreatedDate = DateTime.Now,
            };
            _itemRepository.Create(item);
            var addFields = CreateFields(model.AddItems, collection.AddFields, item);
            _itemRepository.AddFieldRange(addFields);
            item.AddItems = addFields;
            await _itemRepository.SaveChangesAsync();
            return item;
        }

        public List<Tag> CreateTags(List<TagViewModel> tags)
        {
            List<Tag> tagsList = new();
            foreach (var tag in tags)
            {
                tagsList.Add(new() { TagName = tag.TagName });
            }
            return tagsList;
        }

        public List<TagViewModel> CreateTagViewModel(List<Tag> tags)
        {
            List<TagViewModel> tagsList = new();
            foreach (var tag in tags)
            {
                tagsList.Add(new() { TagId = tag.TagId.ToString(), TagName = tag.TagName });
            }
            return tagsList;
        }

        private FieldViewModel CreateFieldViewModel(CustomFieldViewModel custom, string id = "", string value = "")
        {
            FieldViewModel model = new()
            {
                CustomFieldViewModel = custom,
                AddItemFieldId = id,
                Value = value,
            };
            return model;
        }

        private CustomFieldViewModel CreateCustomFieldViewModel(string id, CollectionFieldType type, string name)
        {
            CustomFieldViewModel model = new()
            {
                FieldId = id,
                FieldType = type,
                Name = name,
            };
            return model;
        }

        private List<FieldViewModel> CreateListFieldViewModel(List<AddCollectionField> fields)
        {
            List<FieldViewModel> fieldList = new();
            foreach (var field in fields)
            {
                var custField = CreateCustomFieldViewModel(field.FieldId.ToString(), field.Type, field.Name);
                var fieldView = CreateFieldViewModel(custField);
                fieldList.Add(fieldView);
            }
            return fieldList;
        }

        public ItemViewModel CreateItemViewModel(Collection collection)
        {
            ItemViewModel model = new()
            {
                CollectionId = collection.CollectionId.ToString(),
                AddItems = CreateListFieldViewModel(collection.AddFields),
                Tags = new() { new() { TagName = "#" } },
            };
            return model;
        }

        public async Task<Item?> GetAllItemFieldsAsync(string id)
        {
            var item = await _itemRepository.GetItemAsync(id);
            if (item == null)
                return null;
            return item;
        }

        private List<FieldViewModel> CreateListFieldViewModel(List<AddItemField> fields)
        {
            List<FieldViewModel> fieldList = new();
            foreach (var field in fields)
            {
                var custField = CreateCustomFieldViewModel(field.FieldId.ToString(), field.AddCollectionFields.Type, field.AddCollectionFields.Name);
                var fieldView = CreateFieldViewModel(custField, field.FieldId.ToString(), field.Value);
                fieldList.Add(fieldView);
            }
            return fieldList;
        }

        public ItemViewModel CreateItemViewModel(Item item)
        {
            ItemViewModel model = new()
            {
                CollectionId = item.CollectionId.ToString(),
                ItemId = item.ItemId.ToString(),
                Name = item.Name,
                AddItems = CreateListFieldViewModel(item.AddItems),
                Tags = CreateTagViewModel(item.Tags),
            };
            return model;
        }

        public async Task<Item> UpdateItem(ItemViewModel model, Item item)
        {
            item.Tags.Clear();
            var tags = CreateTags(model.Tags);
            _itemRepository.AddTagRange(tags);
            item.Tags.AddRange(tags);
            item.Name = model.Name;

            for (int i = 0; i < item.AddItems.Count; i++)
            {
                if (item.AddItems[i].AddCollectionFields.Type == CollectionFieldType.dateField)
                {
                    item.AddItems[i].Value = DateTime.Parse(model.AddItems[i].Value).ToShortDateString();
                }
                else
                    item.AddItems[i].Value = model.AddItems[i].Value;
            }
            _itemRepository.Update(item);
            await _itemRepository.SaveChangesAsync();
            return item;
        }

        public IEnumerable<Item>? SortItems(IEnumerable<Item> items, string sortRule="Name")
        {
            if (string.IsNullOrEmpty(sortRule) || items==null)
                return items;
            switch (sortRule)
            {
                case "Name_Desc": return items.OrderByDescending(i => i.Name);
                case "Name": return items.OrderBy(i => i.Name);
                default:
                    {
                        if (sortRule.Contains("_Desc"))
                        {
                            sortRule = sortRule.Replace("_Desc", "");
                            return items.OrderByDescending(i => i.AddItems.Where(ai => ai.AddCollectionFields.Name == sortRule)
                        .Select(ai => ai.Value).ElementAt(0));
                        }
                        return items.OrderBy(i => i.AddItems.Where(ai => ai.AddCollectionFields.Name == sortRule)
                        .Select(ai => ai.Value).ElementAt(0));
                    }
            }
        }
    }
}
