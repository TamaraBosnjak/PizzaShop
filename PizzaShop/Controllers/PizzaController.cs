using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PizzaShop.Models;
using PizzaShop.ViewModels;

namespace PizzaShop.Controllers
{
    public class PizzaController : Controller
    {
        private readonly IPizzaRepository _pizzaRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUserRepository _userRepository;
        private readonly INotyfService _notyf;

        public PizzaController(
            IPizzaRepository pizzaRepository,
            ICategoryRepository categoryRepository,
            IUserRepository userRepository, 
            INotyfService notyf)
        {
            _pizzaRepository = pizzaRepository;
            _categoryRepository = categoryRepository;
            _userRepository = userRepository;
            _notyf = notyf;
        }

        public IActionResult Index(bool uslov, bool uslov2)
        {
            ViewBag.Uslov = uslov;
            ViewBag.Uslov2 = uslov2;
            ViewBag.Message = "Ovo je server-side poruka.";
            ViewBag.Message2 = "Ovo je druga poruka.";
   
            return View();
        }

        public ViewResult List(int? categoryID)
        {
            var userCookie = Request.Cookies["User"];
            var user = JsonConvert.DeserializeObject<User>(userCookie!);

            IEnumerable<Pizza> pizzas;
            string category = "Sve pice";

            var categoryObj = _categoryRepository.GetCategoryByID(categoryID);

            if (categoryObj != null) 
            {
                category = categoryObj.Name;
            }

            if (category == "Sve pice")
            {
                pizzas = _pizzaRepository.Pizzas.OrderBy(p => p.ID).Where(c => c.UserID == null);
            }
            else if (category == "Pice korisnika")
            {
                pizzas = _pizzaRepository.Pizzas.Where(p => p.Category.ID == categoryID && p.UserID == user!.UserID).OrderBy(p => p.ID);
                return View("UserPizzas", new PizzaListViewModel(pizzas, category));
            }
            else
            {
                pizzas = _pizzaRepository.Pizzas.Where(p => p.Category.ID == categoryID).OrderBy(p => p.ID);
            }

            return View(new PizzaListViewModel(pizzas, category));
        }

        public ViewResult Details(int id)
        {
            Pizza p = _pizzaRepository.GetPizzaByID(id);
            return View(new DetailsViewModel(id, p.ImageUrl, p.LongDescription, p.Price));
        }

        public IActionResult YourCustomPizza()
        {
            var vm = new UserCustomPizzaViewModel();
            return View(vm);
        }

        public IActionResult SavePizza(UserCustomPizzaViewModel vm) 
        {
            var userCookie = Request.Cookies["User"];
            var user = JsonConvert.DeserializeObject<User>(userCookie!);

            var pizza = new Pizza() 
            {
                Name = vm.PizzaName,
                Category = _categoryRepository.GetAllCategories().FirstOrDefault(c => c.Name == "Pice korisnika")!,
                LongDescription = vm.Ingredients,
                Price = 1500,
                UserID = user!.UserID,
                ShortDescription = vm.Ingredients,
                ImageThumbnailUrl = string.Empty,
                ImageUrl = string.Empty,
                IsPizzaOfTheWeek = false,
                InStock = true
            };

            _pizzaRepository.SavePizza(pizza);

            _notyf.Success("Uspesno ste sacuvali picu!",3);

            return RedirectToAction("Profile", "User");
        }
    }
}
