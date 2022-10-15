using CollectionsProject.Models.CollectionModels;
using CollectionsProject.Models.UserModels;
using CollectionsProject.ViewModels;

namespace CollectionsProject.Services.Interfaces
{
    public interface ICollectionService
    {
        public Collection CreateNewCollection(CollectionViewModel model, User user);
        public AddCollectionField CreateAddField(CustomFieldViewModel model, Collection collection);
        public List<AddCollectionField> CreateAddFields(List<CustomFieldViewModel> model, Collection collection);
    }
}
