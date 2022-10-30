using CollectionsProject.Models.CollectionModels;
using CollectionsProject.Models.ItemModels;
using System.ComponentModel.DataAnnotations;

namespace CollectionsProject.ViewModels
{
    public class ItemViewModel
    {
        public string ItemId { get; set; } = string.Empty;

        [Required(ErrorMessage = "Name is required")]
        [StringLength(15, ErrorMessage = "Length should be less than 15", MinimumLength = 1)]
        public string Name { get; set; } = "";

        [Required]
        public string CollectionId { get; set; } = null!;

        public List<FieldViewModel> AddItems { get; set; } = new();

        public List<TagViewModel> Tags { get; set; } = new(1);

    }
}
