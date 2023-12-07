using Microsoft.AspNetCore.Mvc;
using PizzaShop.Models;
using PizzaShop.ViewModels;

namespace PizzaShop.Components.ShoppingCartSummary
{
    public class ShoppingCartSummary : ViewComponent
    {
        private readonly IShoppingCart _shoppingCart;
        public ShoppingCartSummary(IShoppingCart shoppingCart)
        {
            _shoppingCart = shoppingCart;
        }
        public IViewComponentResult Invoke()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            var vm = new ShoppingCartViewModel(_shoppingCart, _shoppingCart.GetShoppingCartTotalQuantity(),_shoppingCart.GetShoppingCartTotal());
            return View(vm);
        }
    }
}
