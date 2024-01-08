namespace PizzaShop.Models
{
    public interface IOrderRepository
    {
        void CreateOrder(Order order);
        IQueryable<Order> GetOrdersByUser(int orderID);
    }
}
