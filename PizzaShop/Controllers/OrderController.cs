using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
            var userCookie = Request.Cookies["User"];
            var user = JsonConvert.DeserializeObject<User>(userCookie!);

            var vm = new Order();

            if (user != null)
            {
                vm.Address = user.Address;
                vm.City = user.City;
                vm.Country = user.Country;
                vm.PhoneNumber = user.PhoneNumber;
                vm.FirstName = user.FirstName;
                vm.LastName = user.LastName;
                vm.Email = user.Email;
            }
            return View(vm);
        }

        [HttpPost]
        public IActionResult Checkout(Order order) 
        {
            var item = _shoppingCart.ShoppingCartItems;
            _shoppingCart.ShoppingCartItems = item;
            
            if(_shoppingCart.ShoppingCartItems.Count == 0) 
            {
                ModelState.AddModelError("", "Vasa korpa je prazna!");
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
