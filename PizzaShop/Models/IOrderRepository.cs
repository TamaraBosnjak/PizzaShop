namespace PizzaShop.Models
{
    public interface IOrderRepository
    {
        void CreateOrder(Order order);
        List<Order> GetOrdersByUser(int orderID);
        List<Order> AllOrdersFromEachUser { get; set; } 
    }
}
