using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using WebStore.Models;

namespace WebStore.ViewModels
{
    public class CustomerFormViewModel
    {
        public IEnumerable<Gender> Genders { get; set; }


        public string Id { get; set; }
        
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
        public int? PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Płeć")]
        public int GenderId { get; set; }



        public IFormFile Photo { get; set; }


    }
}
