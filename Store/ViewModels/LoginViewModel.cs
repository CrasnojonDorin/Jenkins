using System.ComponentModel.DataAnnotations;

namespace Store.ViewModels
{
    public class LoginViewModel
    {

        [Required]
        [Display(Name = "Login")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Hasło")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Zapamiętaj mnie")]
        public bool RememberMe { get; set; }

    }
}
