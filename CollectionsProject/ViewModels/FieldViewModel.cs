using CollectionsProject.Models.CollectionModels;
using CollectionsProject.Models.ItemModels;
using System.ComponentModel.DataAnnotations;

namespace CollectionsProject.ViewModels
{
    public class FieldViewModel
    {
        public string AddItemFieldId { get; set; }=string.Empty;

        public string Value { get; set; } = "";

        public CustomFieldViewModel CustomFieldViewModel { get; set; } = new();
    }
}
