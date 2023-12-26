using System.ComponentModel.DataAnnotations;

namespace PizzaShop.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name ="Korisnicko ime")]
        public string UserName { get; set; }
        [Required]
        [Display(Name = "Lozinka")]
        public string Password { get; set; }
    }
}
