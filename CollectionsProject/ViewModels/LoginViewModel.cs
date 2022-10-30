using System.ComponentModel.DataAnnotations;

namespace CollectionsProject.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Enter your Email")]
        [StringLength(20, ErrorMessage = "Length should be less than 20")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Enter your password")]
        [DataType(DataType.Password)]
        [StringLength(15, ErrorMessage = "Length should be less than 15")]
        public string? Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
