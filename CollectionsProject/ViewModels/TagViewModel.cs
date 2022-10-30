using System.ComponentModel.DataAnnotations;

namespace CollectionsProject.ViewModels
{
    public class TagViewModel
    {
        public string TagId { get; set; } = string.Empty;

        [Required(ErrorMessage = "Tag is required")]
        [RegularExpression(@"^#\w{1,10}$", ErrorMessage = "Use only eng letter or numbers, length is up to 10")]
        public string TagName { get; set; } = "";
    }
}
