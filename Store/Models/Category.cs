using System.ComponentModel.DataAnnotations;

namespace Store.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nazwa jest wymagana")]
        [Display(Name = "Nazwa")]
        public string Name { get; set; }

    }
}
