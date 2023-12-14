using Microsoft.EntityFrameworkCore;

namespace PizzaShop.Models
{
    public class ShoppingCart : IShoppingCart
    {
        private readonly AppDBContext _context;
        public string? ShoppingCartID { get; set; }
        public int Amount { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; } = default!;

        public ShoppingCart(AppDBContext context)
        {
            _context = context;
        }

        public static ShoppingCart GetCart(IServiceProvider services)
        {
            ISession? session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext?.Session;
            AppDBContext context = services.GetRequiredService<AppDBContext>() ?? throw new Exception("Error Initializing");
            string cartID = session?.GetString("cartID") ?? Guid.NewGuid().ToString();
            session?.SetString("cartID", cartID);
            return new ShoppingCart(context) { ShoppingCartID = cartID };
        }
        public void AddToCart(Pizza pizza, int amount)
        {
            var shoppingCartItem = _context.ShoppingCartItems.SingleOrDefault(s => s.Pizza.ID == pizza.ID && s.ShoppingCartID == ShoppingCartID);
            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartID = ShoppingCartID!,
                    Pizza = pizza,
                    Amount = 1
                };
                _context.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }
            _context.SaveChanges();
        }
        public int RemoveFromCart(Pizza pizza)
        {
            var shoppingCartItem = _context.ShoppingCartItems.SingleOrDefault(s => s.Pizza.ID == pizza.ID && s.ShoppingCartID == ShoppingCartID);

            var localAmount = 0;

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                    localAmount = shoppingCartItem.Amount;
                }
                else
                {
                    _context.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }

            _context.SaveChanges(true);
            return localAmount;
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ??= _context.ShoppingCartItems.Where(c => c.ShoppingCartID == ShoppingCartID).Include(s => s.Pizza).ToList();
        }
        public decimal GetShoppingCartTotal()
        {
            var total = _context.ShoppingCartItems.Where(c => c.ShoppingCartID == ShoppingCartID).Select(c => c.Pizza.Price * c.Amount).Sum();
            return total;
        }

        public decimal GetShoppingCartTotalQuantity()
        {
            var totalQuantity = _context.ShoppingCartItems.Where(c => c.ShoppingCartID == ShoppingCartID).Select(c => c.Amount).Sum();
            return totalQuantity;
        }
        public void ClearCart()
        {
            var cartItems = _context.ShoppingCartItems.Where(c => c.ShoppingCartID == ShoppingCartID);

            _context.ShoppingCartItems.RemoveRange(cartItems);

            _context.SaveChanges();
        }
    }
}
