using System.ComponentModel.DataAnnotations;

namespace WebStore.Models.DTO
{
    public class UserDTO
    {
        [Display(Name = "Imię")]
        [MaxLength(25)]
        public string FirstName { get; set; }

        [Display(Name = "Nazwisko")]
        [MaxLength(25)]
        public string LastName { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public int PhoneNumber { get; set; }
    }
}
