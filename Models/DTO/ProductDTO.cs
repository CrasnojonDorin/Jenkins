using System.ComponentModel.DataAnnotations;

namespace WebStore.Models.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }


        [Required]
        public int TypeId { get; set; }


        [Required]
        public string Name { get; set; }


        public string PhotoPath { get; set; }


        [Required]
        public double Price { get; set; }

        [Required]
        public int? ColorId { get; set; }

        [Required]
        public int? BrandId { get; set; }

        [Required]
        public int SexId { get; set; }

        [Required]
        public int? SizeId { get; set; }


        public string Description { get; set; }
    }
}
