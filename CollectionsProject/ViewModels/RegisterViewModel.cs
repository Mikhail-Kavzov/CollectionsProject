using System.ComponentModel.DataAnnotations;

namespace CollectionsProject.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Enter your Email")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Enter your name")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Enter your password")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords are not equal")]
        public string? ConfirmPassword { get; set; }
    }
}
