using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.Models
{
    public class Type
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nazwa jest wymagana")]
        [Display(Name = "Nazwa")]
        public string Name { get; set; }

        public virtual List<Size> Size { get; set; }
    }
}
