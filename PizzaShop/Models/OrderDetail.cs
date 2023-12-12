namespace PizzaShop.Models
{
    public class OrderDetail
    {
        public int ID { get; set; }
        public int OrderID { get; set; }
        public int PizzaID { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public Order Order { get; set; } = default!;
        public Pizza Pizza { get; set; } = default!;
    
    }
}
