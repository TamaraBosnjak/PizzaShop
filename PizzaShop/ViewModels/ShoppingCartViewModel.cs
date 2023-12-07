using PizzaShop.Models;

namespace PizzaShop.ViewModels
{
    public class ShoppingCartViewModel
    {
        public IShoppingCart ShoppingCart { get; }
        public decimal ShoppingCartTotal { get; }
        public decimal ShoppingCartTotalQuantity { get; }
        
        
        public ShoppingCartViewModel(IShoppingCart shoppingCart, decimal shoppingCartTotal, decimal shoppingCartTotalQuantity)
        {
            ShoppingCart = shoppingCart;
            ShoppingCartTotal = shoppingCartTotal;
            ShoppingCartTotalQuantity = shoppingCartTotalQuantity;
        }

    }
}
