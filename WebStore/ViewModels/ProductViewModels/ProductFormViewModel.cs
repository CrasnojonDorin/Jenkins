using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebStore.Models;
using Type = WebStore.Models.Type;

namespace WebStore.ViewModels.ProductViewModels
{
    public class ProductFormViewModel
    {
        public List<Type> Types { get; set; }
        public List<Brand> Brands{ get; set; }
        public List<Color> Colors{ get; set; }
        public List<Sex> Sexes { get; set; }
        public List<Size> ShoeSizes{ get; set; }
        public List<Size> ClothSizes { get; set; }




        [Display(Name = "Zdjęcie")]
        public IFormFile Photo { get; set; }


        public int Id { get; set; }

        [Display(Name = "Typ")]
        [Required (ErrorMessage = "Nie wybrano typu")]
        public int TypeId { get; set; }

        [Display(Name = "Nazwa")]
        [Required(ErrorMessage = "Nie podano nazwy")]
        public string Name { get; set; }



        [Display(Name = "Cena")]
        [Required(ErrorMessage = "Nie podano ceny")]
        public double Price { get; set; }

        [Required(ErrorMessage = "Nie wybrano koloru. Jeżeli go nie ma na liście to go dodaj.")]
        [Display(Name = "Kolor")]
        public int? ColorId { get; set; }

        [Required(ErrorMessage = "Nie wybrano brandu. Jeżeli go nie ma na liście, to go dodaj.")]
        [Display(Name = "Brand")]
        public int? BrandId { get; set; }

        [Display(Name = "Płeć")]
        [Required(ErrorMessage = "Nie wybrano płci")]
        public int SexId { get; set; }
        
        [Required(ErrorMessage = "Nie wybrano rozmiaru. Jeżeli go nie ma możesz go na liście, to dodaj go.")]
        [Display(Name = "Rozmiar")]
        public int? SizeId { get; set; }

        [Display(Name = "Opis")]
        public string Description { get; set; }
    }
}
