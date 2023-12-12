using PizzaShop.Models;

namespace PizzaShop.ViewModels
{
    public class PizzaCardViewModel
    {
       public IPizzaRepository PizzaRepository { get; }
        public int Amount { get; }
        public PizzaCardViewModel(IPizzaRepository pizzaRepository, int amount)
        {
            PizzaRepository = pizzaRepository;
            Amount = amount;
        }
    }
}
