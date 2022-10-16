using CollectionsProject.Models.CollectionModels;
using CollectionsProject.Models.ItemModels;
using CollectionsProject.ViewModels;

namespace CollectionsProject.Services.Interfaces
{
    public interface IItemService
    {
        List<AddItemField> CreateFields(List<FieldViewModel> model, List<AddCollectionField> colField, Item item);
        Task<Item> CreateNewItem(ItemViewModel model, Collection collection);
        ItemViewModel CreateItemViewModel(Collection collection);
        ItemViewModel CreateItemViewModel(Item item);
        List<Tag> CreateTags(List<TagViewModel> tags);
        Task<Item?> GetAllItemFieldsAsync(string id);
        List<TagViewModel> CreateTagViewModel(List<Tag> tags);
        Task<Item> UpdateItem(ItemViewModel model, Item item);
    }
}
