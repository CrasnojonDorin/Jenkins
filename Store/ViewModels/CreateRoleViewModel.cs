using System.ComponentModel.DataAnnotations;

namespace Store.ViewModels
{
    public class CreateRoleViewModel
    {
        [Required]
        [Display(Name = "Nazwa")]
        public string RoleName { get; set; }
    }
}
