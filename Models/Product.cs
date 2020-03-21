using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;

namespace WebStore.Models
{
    public class Product
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [Required]
        public int TypeId { get; set; }

        public Type Type{ get; set; }

        [Required]
        public string Name { get; set; }


        public string PhotoPath { get; set; }

        
        [Required]
        public double Price { get; set; }

        public Color Color { get; set; }

        public int? ColorId { get; set; }


        public Brand Brand { get; set; }

        public int? BrandId { get; set; }


        [Required]
        public int SexId { get; set; }

        public Sex Sex { get; set; }

        public int? SizeId { get; set; }

        public Size Size { get; set; }

        public string Description { get; set; }

    }
}
