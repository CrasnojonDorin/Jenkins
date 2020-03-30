using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebStore.Models;

namespace WebStore.ViewModels
{
    public class EditRoleViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nazwa roli jest wymagana")]
        [Display(Name = "Nazwa")]
        public string RoleName { get; set; }

        public List<User> Users { get; set; }
    }
}
