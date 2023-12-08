namespace PizzaShop.Models
{
    public class Category
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<Pizza> Pizzas { get; set; }


    }
}
