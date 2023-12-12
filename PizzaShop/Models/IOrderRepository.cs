namespace PizzaShop.Models
{
    public interface IOrderRepository
    {
        void CreateOrder(Order order);
    }
}
