using PizzaShop.Models;
using System.ComponentModel.DataAnnotations;

namespace PizzaShop.ViewModels
{
    public class UserCustomPizzaViewModel
    {
        [Display(Name = "Ime pice")]
        public string PizzaName { get; set; }
        [Display(Name = "Sastojci")]
        public string Ingredients { get; set; }
    }
}
