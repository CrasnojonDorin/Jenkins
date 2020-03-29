using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebStore.Models;

namespace WebStore.ViewModels
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


        [MaxLength(35)]
        [Display(Name = "Miasto")]
        public string Town { get; set; }

        [Display(Name = "Numer telefonu")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Płeć")]
        public int GenderId { get; set; }

        public IFormFile Photo { get; set; }


    }
}
