using Microsoft.AspNetCore.Mvc;
using PizzaShop.Models;

namespace PizzaShop.Controllers
{
    public class OrderController : Controller
    {
        private readonly IShoppingCart _shoppingCart;
        private readonly IOrderRepository _orderRepository;

        public OrderController(IShoppingCart shoppingCart, IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
            _shoppingCart = shoppingCart;
        }
        public IActionResult Checkout() 
        {
            return View();
        }
        [HttpPost]
        public IActionResult Checkout(Order order) 
        {
            var item = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = item;
            
            if(_shoppingCart.ShoppingCartItems.Count == 0) 
            {
            }
            if(ModelState.IsValid)
            {
                _orderRepository.CreateOrder(order);
                _shoppingCart.ClearCart();

                return RedirectToAction ("CheckoutComplete");
            }
            return View(order);
        }
        public IActionResult CheckoutComplete() 
        {
            ViewBag.Message = "Uspesna porudzbina!";
            return View();
        }
    }
}
