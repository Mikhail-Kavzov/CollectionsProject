using CollectionsProject.Models.CollectionModels;
using CollectionsProject.Models.ItemModels;
using CollectionsProject.Services.Interfaces;
using CollectionsProject.ViewModels;

namespace CollectionsProject.Services.Implementation
{
    public class ItemService:IItemService
    {
        private static readonly Dictionary<string, string> checkBoxState = new()
        {
            {"on","Yes"},
            {"","No"},
        };

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
                switch (model[i].CustomFieldViewModel.FieldType)
                {
                    case CollectionFieldType.booleanField:
                        model[i].Value = checkBoxState[model[i].Value];
                        break;
                    case CollectionFieldType.dateField:
                        {
                            model[i].Value=DateTime.Parse(model[i].Value).ToShortDateString();
                            break;
                        }
                }
                addItemFields.Add(CreateAddField(model[i].Value, colField[i], item));
            }
            return addItemFields;
        }

        public Item CreateNewItem(string name, Collection collection, List<Tag> tags)
        {
            Item item = new()
            {
                Name = name,
                Collection = collection,
                Tags = tags,
            };
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


        public FieldViewModel CreateFieldViewModel(CustomFieldViewModel custom, string id = "", string value = "")
        {
            FieldViewModel model = new()
            {
                CustomFieldViewModel = custom,
                AddItemFieldId = id,
                Value = value,
            };
            return model;
        }

        public CustomFieldViewModel CreateCustomFieldViewModel(string id, CollectionFieldType type, string name)
        {
            CustomFieldViewModel model = new()
            {
                FieldId = id,
                FieldType = type,
                Name = name,
            };
            return model;
        }

        public ItemViewModel CreateItemViewModel(Collection collection)
        {
            ItemViewModel model = new()
            {
                CollectionId = collection.CollectionId.ToString(),
            };
            if (collection.AddFields == null)
                throw new ArgumentNullException($"No additional fields in collection: {collection.Name}");
            foreach (var field in collection.AddFields)
            {
                var customModel = CreateCustomFieldViewModel(field.FieldId.ToString(), field.Type, field.Name);
                var fieldModel = CreateFieldViewModel(customModel);
                model.AddItems.Add(fieldModel);
            }
            model.Tags = new() { new() { TagName = "#" } };
            return model;
        }
    }
}
