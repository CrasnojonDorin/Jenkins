using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebStore.Models
{
    public class Brand
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }

        public string LogoPath { get; set; }

        public string Description { get; set; }

        public virtual List<Product> Products { get; set; }
    }
}
