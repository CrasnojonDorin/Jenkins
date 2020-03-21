using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace WebStore.Models
{
    public class Customer
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        public User User { get; set; }

        [Required]
        [Display(Name = "Płeć")]
        public int GenderId { get; set; }

        [Display(Name = "Płeć")]
        public Gender Gender { get; set; }


        [Display(Name = "Numer telefonu")]
        [DataType(DataType.PhoneNumber)]
        public int? PhoneNumber { get; set; }

        [Display(Name = "Miasto")]
        [MaxLength(30)]
        public string Town { get; set; }


        public string PhotoPath { get; set; }


    }
}
