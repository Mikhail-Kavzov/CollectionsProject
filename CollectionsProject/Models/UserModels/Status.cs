using System.ComponentModel.DataAnnotations;

namespace CollectionsProject.Models.UserModels
{
    public enum Status
    {
        [Display(Name = "Active")]
        Active,
        [Display(Name = "Blocked")]
        Blocked,
    }
}
