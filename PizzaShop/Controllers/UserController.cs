using Microsoft.AspNetCore.Mvc;
using PizzaShop.Models;
using PizzaShop.ViewModels;

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

        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                var isExist = _userRepository.IsExist(user.UserName);

                if (!isExist)
                {
                    _userRepository.CreateUser(user);

                    return View("Success");
                }
                else
                {
                    ModelState.AddModelError("", "Korisnicko ime " + user.UserName + " je zauzeto");
                    return View("Index", user);
                }
            }
            else
            {
                return View("Index", user);
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

        public IActionResult SignIn(LoginViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return View("Login", user);
            }

            var isExist = _userRepository.IsExist(user.UserName);

            if (isExist) 
            {
                var isPasswordOK = _userRepository.IsPasswordOK(user.Password);

                if (isPasswordOK) 
                {
                    return RedirectToAction("SignInSuccess");
                }
            }

            ModelState.AddModelError("", "Neispravni kredencijali");
            return View("Login", user);
        }
        public IActionResult SignInSuccess()
        {
            //ViewBag.Message = "Uspesno ste se ulogovali!";
            return View();
        }
    }
}
