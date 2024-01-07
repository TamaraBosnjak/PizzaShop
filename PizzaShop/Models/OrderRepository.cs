using Microsoft.EntityFrameworkCore;
using System.Data.Entity;
using System.Linq;

namespace PizzaShop.Models
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDBContext _context;
        private readonly IShoppingCart _shoppingCart;
        //public int OrderID { get; set; }

        public List<Order> AllOrdersFromEachUser { get; set; } = default!;

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
        public List<Order> GetOrdersByUser(int userID)
        {
            return AllOrdersFromEachUser = _context.Orders.Where(o => o.UserID == userID).ToList();
        }
        //public Order GetOrdersWithPizzasByUserID(int ID)
        //{
        //    return _context.Orders
        //        .FirstOrDefault(u => u.ID == ID)
        //        .Include(u => u.OrderDetails)
        //        .ThenInclude(od => od.Pizza);
        //}
    }
}
