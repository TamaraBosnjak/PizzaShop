﻿using Microsoft.AspNetCore.Mvc;
using PizzaShop.Models;
using PizzaShop.ViewModels;
using System.Diagnostics;

namespace PizzaShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPizzaRepository _pizzaRepository;
        private readonly ShoppingCartItemRepository _shoppingCartItemRepository;

        public HomeController(ILogger<HomeController> logger, IPizzaRepository pizzaRepository, ShoppingCartItemRepository shoppingCartItemRepository)
        {
            _logger = logger;
            _pizzaRepository = pizzaRepository;
            _shoppingCartItemRepository = shoppingCartItemRepository;

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

        public ViewResult ShoppingCart(string shoppingCartID)
        {
            var shoppingItem = _shoppingCartItemRepository.GetShoppingCartItemByID("");
            return View(shoppingItem);  
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