using System.ComponentModel.DataAnnotations;

namespace CollectionsProject.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Enter your Email")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Enter your password")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
