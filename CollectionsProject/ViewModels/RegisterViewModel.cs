using System.ComponentModel.DataAnnotations;

namespace CollectionsProject.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Enter your Email")]
        [StringLength(20, ErrorMessage = "Length should be less than 20")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Enter your name")]
        [StringLength(15, ErrorMessage = "Length should be less than 15")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Enter your password")]
        [DataType(DataType.Password)]
        [StringLength(15, ErrorMessage = "Length should be less than 15")]
        public string? Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords are not equal")]
        public string? ConfirmPassword { get; set; }
    }
}
