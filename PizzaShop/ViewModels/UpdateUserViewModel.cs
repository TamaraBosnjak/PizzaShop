using System.ComponentModel.DataAnnotations;

namespace PizzaShop.ViewModels
{
    public class UpdateUserViewModel
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Korisnicko ime")]
        public string UserName { get; set; }
        [Required]
        [Display(Name = "Trenutna sifra")]
        public string CurrentPassword { get; set; }
        [Required]
        [Display(Name = "Nova sifra")]
        public string NewPassword { get; set; }
        [Required]
        [Display(Name = "Potvrdi novu sifra")]
        public string ConfirmPassword { get; set; }
    }
}
