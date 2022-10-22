using CollectionsProject.Models.CollectionModels;
using CollectionsProject.Models.ItemModels;
using CollectionsProject.Models.UserModels;
using CollectionsProject.ViewModels;

namespace CollectionsProject.Services.Interfaces
{
    public interface ICollectionService
    {
        Collection CreateNewCollection(CollectionViewModel model, User user);
        AddCollectionField CreateAddField(CustomFieldViewModel model, Collection collection);
        List<AddCollectionField> CreateAddFields(List<CustomFieldViewModel> model, Collection collection);
        Task<int> GetItemCountAsync(string collectionId);
    }
}
