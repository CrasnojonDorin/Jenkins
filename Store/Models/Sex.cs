using System.ComponentModel.DataAnnotations;

namespace Store.Models
{
    public class Sex
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
