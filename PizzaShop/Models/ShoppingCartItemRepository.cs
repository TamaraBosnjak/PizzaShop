using Microsoft.EntityFrameworkCore;

namespace PizzaShop.Models
{
    public class ShoppingCartItemRepository
    {
        private AppDBContext _appDBContext;
        public ShoppingCartItemRepository(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;

        }
        public IEnumerable<ShoppingCartItem> GetShoppingCartItemByID(string shoppingCartID)
        {
            return _appDBContext.ShoppingCartItems.Include(p => p.Pizza).Where(p => p.ShoppingCartID == shoppingCartID);
        }
    }
}
