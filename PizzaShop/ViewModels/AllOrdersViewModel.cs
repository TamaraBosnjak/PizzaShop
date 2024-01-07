using PizzaShop.Models;

namespace PizzaShop.ViewModels
{
    public class AllOrdersViewModel
    {
        public IOrderRepository OrderRepository { get; }
        public List<Order> GetOrdersByUser { get; }

        public AllOrdersViewModel(IOrderRepository orderRepository, List<Order> getOrdersByUser)
        {
            OrderRepository = orderRepository;
            GetOrdersByUser = getOrdersByUser;
        }
    }
}
