using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PizzaShop.Models;
using PizzaShop.ViewModels;

namespace PizzaShop.Controllers
{
    public class OrderController : Controller
    {
        private readonly IShoppingCart _shoppingCart;
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;

        public OrderController(IShoppingCart shoppingCart, IOrderRepository orderRepository, IUserRepository userRepository)
        {
            _orderRepository = orderRepository;
            _shoppingCart = shoppingCart;
            _userRepository = userRepository;
        }
        public IActionResult Checkout()
        {
            var userCookie = Request.Cookies["User"];

            if (userCookie == null)
            {
                return RedirectToAction("Login", "User");
            }

            var user = JsonConvert.DeserializeObject<User>(userCookie!);
            var userWithPizzas = _userRepository.GetUsersWithPizzasByUserID(user.UserID);

            var vm = new Order();

                vm.UserID = user.UserID;
                vm.Address = user.Address;
                vm.City = user.City;
                vm.Country = user.Country;
                vm.PhoneNumber = user.PhoneNumber;
                vm.FirstName = user.FirstName;
                vm.LastName = user.LastName;
                vm.Email = user.Email;
            
            return View(vm);
        }

        [HttpPost]
        public IActionResult Checkout(Order order) 
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;
            
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

        public IActionResult AllOrders()
        { 
            var userCookie = Request.Cookies["User"];
            var user = JsonConvert.DeserializeObject<User>(userCookie!);

            var vm = new UserOrdersViewModel();
            vm.UserOrders = _orderRepository.GetOrdersByUser(user!.UserID).ToList();

            return View(vm);
        }
    }
}
