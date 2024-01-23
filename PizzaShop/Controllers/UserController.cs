using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PizzaShop.Helpers;
using PizzaShop.Models;
using PizzaShop.TagHelpers;
using PizzaShop.ViewModels;
using User = PizzaShop.Models.User;

namespace PizzaShop.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly INotyfService _notyf;

        public UserController(IUserRepository userRepository, INotyfService notyf)
        {
            _userRepository = userRepository;
            _notyf = notyf;
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
                    _notyf.Success("Registracija uspesna!", 3);
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

        //[TypeFilter(typeof(UserExceptionFilter))]
        public IActionResult SignIn(LoginViewModel loginUser)
        {
            if (!ModelState.IsValid)
            {
                return View("Login", loginUser);
            }

            var user = _userRepository.GetUserByUsername(loginUser.UserName);
            //throw new Exception($"Nepostojeci username {user.UserName}");


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

                    _notyf.Success("Uspesno ste se ulogovali!", 3);

                    return RedirectToAction("Index", "Home");
                }
            }

            _notyf.Error("Neispravni kredencijali!", 3);

            return View("Login");
        }
        public IActionResult Logout()
        {
            if (HttpContext!.User!.Identity!.IsAuthenticated)
            {
                Response.Cookies.Delete("User");
            }

            _notyf.Success("Uspesno ste se izlogovali!", 3);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Profile() 
        {
            User user = new User();

            if (HttpContext.User.Identity.IsAuthenticated) 
            {
                var userCookie = HttpContext!.Request.Cookies["User"];
                user = JsonConvert.DeserializeObject<User>(userCookie)!;
            }

            var vm = new UpdateUserViewModel()
            {
                ID = user.UserID,
                UserName = user.UserName
            };

            return View(vm);
        }
        public IActionResult ChangeProfile(UpdateUserViewModel model)
        {
            var user = _userRepository.GetUserByID(model.ID);

            if (model.UserName == user.UserName)
            {
                ModelState.AddModelError("", "Korisnicko ime nije ispravno");
                return View("Profile", model);
            }

            if(user.Password == EncryptionHelper.Encrypt(model.CurrentPassword)) 
            {
                _userRepository.UpdatePassword(user, model.NewPassword);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Password nije ispravan");
                return View("Profile", model);
            }
        }
    }
}