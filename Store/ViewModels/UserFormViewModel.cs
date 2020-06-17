using Microsoft.AspNetCore.Http;
using Store.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Store.ViewModels
{
    public class UserFormViewModel
    {
        public IEnumerable<Gender> Genders { get; set; }

        public int Id { get; set; }
        
        [Required]
        [Display(Name = "Imię")]
        [MaxLength(25)]
        public string FirstName { get; set; }
        
        [Required]
        [Display(Name = "Nazwisko")]
        [MaxLength(25)]
        public string LastName { get; set; }



        [Display(Name = "Numer telefonu")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Płeć")]
        public int GenderId { get; set; }

        public IFormFile Photo { get; set; }
        public string PhotoPath { get; set; }


        [Display(Name = "Adres 1")]
        public string Address1 { get; set; }

        [Display(Name = "Adres 2")]
        public string Address2 { get; set; }

        [Display(Name = "Miasto")]
        public string City { get; set; }

        [Display(Name = "Kod pocztowy")]
        public string PostCode { get; set; }

        public string Email { get; set; }



    }
}
