using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Type = WebStore.Models.Type;

namespace WebStore.ViewModels.ProductViewModels
{
    public class SizeFormViewModel
    {
        [MaxLength(15)]
        [Required(ErrorMessage = "Nazwa jest wymagana")]
        [Display(Name = "Nazwa")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Wybierz typ dla którego ten rozmiar jest przeznaczony")]
        [Display(Name = "Typ")]
        public int TypeId { get; set; }
        public List<Type> Types { get; set; }
    }
}
