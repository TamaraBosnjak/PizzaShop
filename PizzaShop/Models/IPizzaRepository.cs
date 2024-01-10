namespace PizzaShop.Models
{
    public interface IPizzaRepository
    {
        Pizza GetPizzaByID(int ID);
        IEnumerable<Pizza> Pizzas { get; }
        //Pizza AddUsersCustomPizza(int userID);
    }
}
