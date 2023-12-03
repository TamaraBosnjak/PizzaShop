namespace PizzaShop.Models
{
    public class Order
    {
        public int ID { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime OrderPlaced { get; set; }
        public decimal OrderTotal { get; set; }
        public string PhoneNumber { get; set; }
        public IEnumerable<OrderDetail> OrderDetails { get; set; }
    }
}
