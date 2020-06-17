using Microsoft.AspNetCore.Http;
using Store.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Store.ViewModels
{
    public class ProductFormViewModel
    {
        public List<Category> Categories { get; set; }
        public List<Brand> Brands { get; set; }
        public List<Color> Colors { get; set; }
        public List<Sex> Sexes { get; set; }




        [Display(Name = "Zdjęcie")]
        public IFormFile Photo { get; set; }

        [Display(Name = "Zdjęcie")]
        public string PhotoPath { get; set; }


        public int Id { get; set; }

        [Display(Name = "Kategoria")]
        [Required(ErrorMessage = "Nie wybrano typu")]
        public int CategoryId { get; set; }

        [Display(Name = "Nazwa")]
        [Required(ErrorMessage = "Nie podano nazwy")]
        public string Name { get; set; }


        [Display(Name = "Cena")]
        [Required(ErrorMessage = "Nie podano ceny")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [RegularExpression("([0-9,]+)", ErrorMessage ="Cena musi być liczbą. Użyj , zamiast .")]

        public string Price { get; set; }

        [Required(ErrorMessage = "Nie wybrano koloru. Jeżeli go nie ma na liście to go dodaj.")]
        [Display(Name = "Kolor")]
        public int? ColorId { get; set; }

        [Required(ErrorMessage = "Nie wybrano brandu. Jeżeli go nie ma na liście, to go dodaj.")]
        [Display(Name = "Brand")]
        public int? BrandId { get; set; }

        [Display(Name = "Płeć")]
        [Required(ErrorMessage = "Nie wybrano płci")]
        public int SexId { get; set; }

        [Display(Name = "Opis")]
        public string Description { get; set; }
    }
}