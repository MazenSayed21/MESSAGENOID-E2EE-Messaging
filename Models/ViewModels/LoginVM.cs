using System.ComponentModel.DataAnnotations;

namespace MESSAGENOID.Models.ViewModels
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string passWord { get; set; }

        [Display(Name = "Remember me?")]
        public bool rememberMe { get; set; }
    }
}
