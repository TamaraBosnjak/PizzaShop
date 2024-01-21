using Microsoft.AspNetCore.Mvc;

namespace PizzaShop.Models
{
    public interface IPizzaRepository
    {
        Pizza GetPizzaByID(int ID);
        void SavePizza(Pizza pizza);
        IEnumerable<Pizza> Pizzas { get; }
        List<Pizza> GetUserPizzas(int userID);
        void DeletePizza(int id);
        void EditPizza(Pizza pizza);
    }
}
