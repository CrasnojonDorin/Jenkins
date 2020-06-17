using Store.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Store.ViewModels
{
    public class RegisterViewModel
    {
        public IEnumerable<Gender> Genders { get; set; }
        
        

        [Display(Name = "Imię")]
        public string FirstName { get; set; }

        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [PhoneAttribute]
        [Required]
        public string Phone { get; set; }

        [Required]
        [Display(Name = "Login")]
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

        [Display(Name = "Adres 1")]
        public string Address1 { get; set; }

        [Display(Name = "Adres 2")]
        public string Address2 { get; set; }

        [Required]
        [Display(Name = "Miasto")]
        public string City { get; set; }

        [Required]
        [Display(Name = "Kod pocztowy")]
        public string PostCode { get; set; }

    }
}
