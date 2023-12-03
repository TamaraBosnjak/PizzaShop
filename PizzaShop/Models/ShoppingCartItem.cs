namespace PizzaShop.Models
{
    public class ShoppingCartItem
    {
        public int ID { get; set; }
        public int Amount { get; set; }
        public string ShoppingCartID { get; set; }
        public Pizza Pizza { get; set; }
    }
}
