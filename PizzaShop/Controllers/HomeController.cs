﻿ using Microsoft.AspNetCore.Mvc;
using PizzaShop.Models;
using PizzaShop.ViewModels;
using System.Diagnostics;

namespace PizzaShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPizzaRepository _pizzaRepository;
        private readonly IShoppingCart _shoppingCart;

        public HomeController(ILogger<HomeController> logger, IPizzaRepository pizzaRepository, IShoppingCart shoppingCart)
        {
            _logger = logger;
            _pizzaRepository = pizzaRepository;
            _shoppingCart = shoppingCart;

        }

        public IActionResult Index()
        {
            var pizzasOfTheWeek = _pizzaRepository.Pizzas.Where(p => p.IsPizzaOfTheWeek == true);
            return View(new HomeViewModel(pizzasOfTheWeek));
        }
        public ViewResult Pretplata()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}