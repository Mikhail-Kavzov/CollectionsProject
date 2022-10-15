using System.ComponentModel.DataAnnotations;

namespace CollectionsProject.Models.UserModels
{
    public enum Role
    {
        [Display(Name = "User")]
        User,
        [Display(Name = "Admin")]
        Admin,
    }
}
