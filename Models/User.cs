using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace WebStore.Models
{
    public class User : IdentityUser

    {
        [Display(Name = "Imię")]
        [MaxLength(25)]
        public string FirstName { get; set; }

        [Display(Name = "Nazwisko")]
        [MaxLength(25)]
        public string LastName { get; set; }

        public Customer Customer { get; set; }



    }
}
