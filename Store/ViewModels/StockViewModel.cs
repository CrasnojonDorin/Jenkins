using Store.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Store.ViewModels
{
    public class StockViewModel
    {
        public int Id { get; set; }

        [Display(Name= "Rozmiar")]
        [Required]
        public string Name { get; set; }

        [Display(Name= "Ilość")]
        [Range(0, 10_000)]
        [Required]
        public int Qty { get; set; }

        [Display(Name = "Produkt")]
        [Required]
        public int ProductId { get; set; }

        public List<Stock> Stock { get; set; }


    }
}
