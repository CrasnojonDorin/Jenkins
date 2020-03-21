using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using WebStore.Models;
using Type = WebStore.Models.Type;

namespace WebStore.ViewModels
{
    public class ProductFormViewModel
    {
        public List<Type> Types { get; set; }
        public List<Brand> Brands{ get; set; }
        public List<Color> Colors{ get; set; }
        public List<Sex> Sexes { get; set; }
        public List<Size> Sizes{ get; set; }

        [Display(Name = "Zdjęcie")]
        public IFormFile Photo { get; set; }




        public int Id { get; set; }

        [Display(Name = "Typ")]
        [Required]
        public int TypeId { get; set; }

        [Display(Name = "Nazwa")]
        [Required]
        public string Name { get; set; }



        [Display(Name = "Cena")]
        [Required]
        public double Price { get; set; }

        [Display(Name = "Kolor")]
        public int? ColorId { get; set; }

        [Display(Name = "Marka")]
        public int? BrandId { get; set; }

        [Display(Name = "Płeć")]
        [Required]
        public int SexId { get; set; }

        [Display(Name = "Rozmiar")]
        public int? SizeId { get; set; }

        [Display(Name = "Opis")]
        public string Description { get; set; }
    }
}
