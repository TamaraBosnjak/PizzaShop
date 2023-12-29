using Azure;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PizzaShop.Helpers;
using PizzaShop.Models;
using PizzaShop.ViewModels;
using System.Web.Providers.Entities;
using User = PizzaShop.Models.User;

namespace PizzaShop.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register(User registerUser)
        {
            if (ModelState.IsValid)
            {
                var user = _userRepository.GetUserByUsername(registerUser.UserName);

                if (user == null)
                {
                    _userRepository.CreateUser(registerUser);
                    return View("Success");
                }
                else
                {
                    ModelState.AddModelError("", "Korisnicko ime " + registerUser.UserName + " je zauzeto");
                    return View("Index", registerUser);
                }
            }
            else
            {
                return View("Index", registerUser);
            }
        }
        public IActionResult Success()
        {
            return View();
        }
        public IActionResult Login()
        {
            var vm = new LoginViewModel();
            return View(vm);
        }
        public IActionResult SignIn(LoginViewModel loginUser)
        {
            if (!ModelState.IsValid)
            {
                return View("Login", loginUser);
            }

            var user = _userRepository.GetUserByUsername(loginUser.UserName);

            if (user != null)
            {
                var isPasswordOk = EncryptionHelper.Encrypt(loginUser.Password) == user.Password ? true : false;

                if (isPasswordOk)
                {
                    user.Password = "";
                    var cookieOptions = new CookieOptions();
                    cookieOptions.Expires = DateTime.Now.AddDays(1);
                    var serializedUser = JsonConvert.SerializeObject(user);
                    Response.Cookies.Append("User", serializedUser, cookieOptions);

                    return RedirectToAction("Index", "Home");
                }
            }
            ModelState.AddModelError("", "Neispravni kredencijali");

            return View("Login");
        }
        public IActionResult Logout()
        {
            Response.Cookies.Delete("User");
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Profile() 
        {
            return View();
        }
        public IActionResult ChangeProfile()
        {
            return View();
        }
    }
}
