using System.ComponentModel.DataAnnotations;

namespace WebStore.Models
{
    public class Size
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(15)]
        public string Name { get; set; }

        [Required]
        public int TypeId { get; set; }

    }
}
