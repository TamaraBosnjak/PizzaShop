namespace PizzaShop.Models
{
    public interface IPizzaRepository
    {
        Pizza GetPizzaByID(int ID);
        void SavePizza(Pizza pizza);
        IEnumerable<Pizza> Pizzas { get; }
    }
}
