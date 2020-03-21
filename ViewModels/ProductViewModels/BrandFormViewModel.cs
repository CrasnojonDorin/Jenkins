using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using WebStore.Models;

namespace WebStore.ViewModels.ProductViewModels
{
    public class BrandFormViewModel
    {
        public int Id { get; set; }

        [MinLength(2)]
        [MaxLength(25)]
        [Display(Name = "Nazwa")]
        [Required(ErrorMessage = "Nazwa jest wymagana")]
        public string Name { get; set; }

        [MinLength(10)]
        [MaxLength(200)]
        [Display(Name = "Opis")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Logo jest wymagane")]
        [Display(Name = "Logo")]
        public IFormFile Photo { get; set; }

        public virtual List<Product> Products { get; set; }
    }
}
