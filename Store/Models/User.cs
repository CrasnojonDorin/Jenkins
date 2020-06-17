using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Store.Models
{
    public class Role : IdentityRole<int> { }


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


        public string PhotoPath { get; set; }

        [Display(Name = "Adres 1")]
        public string Address1 { get; set; }

        [Display(Name = "Adres 2")]
        public string Address2 { get; set; }

        [Display(Name = "Miasto")]
        public string City { get; set; }

        [Display(Name = "Kod pocztowy")]
        public string PostCode { get; set; }
        public virtual List<Order> Orders { get; set; }

    }
}
