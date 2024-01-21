using PizzaShop.Models;
using System.ComponentModel.DataAnnotations;

namespace PizzaShop.ViewModels
{
    public class UserCustomPizzaViewModel
    {
        [Display(Name = "Naziv pice")]
        public string PizzaName { get; set; }

        [Display(Name = "Sastojci")]
        public string Ingredients { get; set; }

        public string AllowedIngredients { get; set; }

        public int PizzaID { get; set; }
    }
}
