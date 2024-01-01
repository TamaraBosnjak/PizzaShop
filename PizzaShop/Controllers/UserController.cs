﻿using Azure;
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

                    return RedirectToAction("Profile", "User");
                }
            }
            ModelState.AddModelError("", "Neispravni kredencijali");

            return View("Login");
        }
        public IActionResult Logout()
        {
            if (Request.Cookies["User"] != null)
            {
                Response.Cookies.Delete("User");
            }
           
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Profile() 
        {
            User user = new User();
            var userCookie = HttpContext!.Request.Cookies["User"];

            if (userCookie != null) 
            {
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