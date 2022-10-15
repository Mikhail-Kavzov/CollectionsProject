using CollectionsProject.Models.CollectionModels;
using CollectionsProject.Models.UserModels;
using CollectionsProject.Services.Interfaces;
using CollectionsProject.ViewModels;

namespace CollectionsProject.Services.Implementation
{
    public class CollectionService:ICollectionService
    {
        public Collection CreateNewCollection(CollectionViewModel model, User user)
        {
            Collection collection = new()
            {
                Image = model.Image,
                Type = model.Type,
                Description = model.Description,
                Name = model.Name,
                User = user,
                Count = 0,
            };
            return collection;
        }

        public AddCollectionField CreateAddField(CustomFieldViewModel model, Collection collection)
        {
            AddCollectionField field = new()
            {
                Name = model.Name,
                Type = model.FieldType,
                Collection = collection,
            };
            return field;
        }

        public List<AddCollectionField> CreateAddFields(List<CustomFieldViewModel> model, Collection collection)
        {
            List<AddCollectionField> customFields = new();
            foreach (var customField in model)
            {
                customFields.Add(CreateAddField(customField, collection));
            }
            return customFields;
        }
    }
}
