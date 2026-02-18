using System.ComponentModel.DataAnnotations;

namespace MESSAGENOID.Models.ViewModels
{
    public class RegisterVM
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Display Name")]
        public string userName { get; set; }

        // This is where the JavaScript-generated RSA Public Key goes
        [Required]
        public string PublicKey { get; set; }
    }
}
