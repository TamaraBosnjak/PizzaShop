using Microsoft.EntityFrameworkCore;

namespace PizzaShop.Models
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDBContext _context;
        private readonly IShoppingCart _shoppingCart;

        public OrderRepository(AppDBContext context, IShoppingCart shoppingCart)
        {
            _context = context;
            _shoppingCart = shoppingCart;
        }

        public void CreateOrder(Order order) 
        {
            order.OrderPlaced = DateTime.Now;
            var shoppingCartItems = _shoppingCart.ShoppingCartItems;
            order.OrderTotal = _shoppingCart.GetShoppingCartTotal();
            order.OrderDetails = new List<OrderDetail>();

            foreach (var item in shoppingCartItems)
            {
                var orderDetail = new OrderDetail
                {
                    Amount = item.Amount,
                    PizzaID = item.Pizza.ID,
                    Price = item.Pizza.Price
                };
                order.OrderDetails.Add(orderDetail);
            }
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public IQueryable<Order> GetOrdersByUser(int userID)
        {
            return _context.Orders
                .Include(p => p.OrderDetails)
                .ThenInclude(od => od.Pizza)
                .Where(o => o.UserID == userID);
        }
    }
}
