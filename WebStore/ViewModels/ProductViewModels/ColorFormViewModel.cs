using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebStore.Models;

namespace WebStore.ViewModels.ProductViewModels
{
    public class ColorFormViewModel
    {
        [Required(ErrorMessage = "Nazwa koloru jest wymagana")]
        [Display(Name = "Nazwa")]
        public string Name { get; set; } 
        public List<Color> Colors { get; set; }
    }
}
