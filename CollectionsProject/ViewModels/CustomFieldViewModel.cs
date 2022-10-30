using CollectionsProject.Models.CollectionModels;
using System.ComponentModel.DataAnnotations;

namespace CollectionsProject.ViewModels
{
    public class CustomFieldViewModel
    {
        public string FieldId { get; set; } = string.Empty;

        [Required]
        public CollectionFieldType FieldType { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(15, ErrorMessage = "Length should be less than 15", MinimumLength = 1)]
        public string Name { get; set; } = "";
    }
}
