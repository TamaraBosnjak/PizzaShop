using PizzaShop.Models;

namespace PizzaShop.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Pizza> PizzasOfTheWeek { get; set; }

        public HomeViewModel(IEnumerable<Pizza> pizzasOfTheWeek)
        {
            PizzasOfTheWeek = pizzasOfTheWeek;
        }
    }
}
