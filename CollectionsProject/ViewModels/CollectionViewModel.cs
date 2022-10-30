using CollectionsProject.Models.CollectionModels;
using System.ComponentModel.DataAnnotations;

namespace CollectionsProject.ViewModels
{
    public class CollectionViewModel
    {
        public string? Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MaxLength(30, ErrorMessage = "Length should be less than 30")]
        public string Name { get; set; } = "";

        [Required(ErrorMessage = "Description is required")]
        [StringLength(300, ErrorMessage = ("Length should be less than 300"), MinimumLength = 1)]
        public string Description { get; set; } = "";

        [Required]
        public CollectionType Type { get; set; }

        public string? Image { get; set; }

        public List<CustomFieldViewModel>? CustomFields { get; set; }
    }
}
