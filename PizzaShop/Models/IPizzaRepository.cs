using System.Collections.Generic;

namespace PizzaShop.Models
{
    public interface IPizzaRepository
    {
        Pizza GetPizzaByID(int ID);
        IEnumerable<Pizza> Pizzas { get; }
    }
}
