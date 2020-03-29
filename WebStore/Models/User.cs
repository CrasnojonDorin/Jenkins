using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebStore.Models
{
    public class IntRole : IdentityRole<int> { }


    public class User : IdentityUser<int>

    {
        [Required]
        [Display(Name = "Imię")]
        [MaxLength(25)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Nazwisko")]
        [MaxLength(25)]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Płeć")]
        public int GenderId { get; set; }

        [Display(Name = "Płeć")]
        public Gender Gender { get; set; }


        [Display(Name = "Numer telefonu")]
        [DataType(DataType.PhoneNumber)]
        public override string PhoneNumber { get; set; }

        [Display(Name = "Miasto")]
        [MaxLength(30)]
        public string Town { get; set; }

        public string PhotoPath { get; set; }
    }
}
