using CollectionsProject.Models.CollectionModels;
using CollectionsProject.Models.ItemModels;
using CollectionsProject.ViewModels;

namespace CollectionsProject.Services.Interfaces
{
    public interface IItemService
    {
        AddItemField CreateAddField(string value, AddCollectionField collectionField, Item item);
        List<AddItemField> CreateFields(List<FieldViewModel> model, List<AddCollectionField> colField, Item item);
        Item CreateNewItem(string name, Collection collection, List<Tag> tags);
        FieldViewModel CreateFieldViewModel(CustomFieldViewModel custom, string id = "", string value = "");
        CustomFieldViewModel CreateCustomFieldViewModel(string id, CollectionFieldType type, string name);
        ItemViewModel CreateItemViewModel(Collection collection);
        List<Tag> CreateTags(List<TagViewModel> tags);
    }
}
