using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebStore.Models;

namespace WebStore.ViewModels
{
    public class RegisterViewModel
    {
        public IEnumerable<Gender> Genders { get; set; }
        
        

        [Display(Name = "Imię")]
        public string FirstName { get; set; }

        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(15)]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(4)]
        [MaxLength(25)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potwierdź Hasło")]
        [Compare("Password",
            ErrorMessage = "Hasła w różnią się")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Płeć")]
        public int GenderId { get; set; }

    }
}
