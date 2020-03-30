using System.ComponentModel.DataAnnotations;

namespace WebStore.ViewModels
{
    public class CreateRoleViewModel
    {
        [Required]
        [Display(Name = "Nazwa")]
        public string RoleName { get; set; }
    }
}
