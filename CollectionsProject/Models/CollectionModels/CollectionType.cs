using System.ComponentModel.DataAnnotations;

namespace CollectionsProject.Models.CollectionModels
{
    public enum CollectionType
    {
        [Display(Name = "Book")]
        Book,
        [Display(Name = "Sign")]
        Sign,
        [Display(Name = "Silverware")]
        Silverware,
    }
}
