using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using PizzaShop.Models;
using PizzaShop.TagHelpers;
using PizzaShop.ViewModels;
using System.Linq;
using System.Reflection.PortableExecutable;

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
            vm.AllowedIngredients = "Sunka, Pecenica, Pelat, Sir, Masline, Pecurke, Jaje, Kukuruz, Paprika, Brokoli";

            return View(vm);
        }

        [TypeFilter(typeof(CustomExceptionFilter))]
        public IActionResult SavePizza(UserCustomPizzaViewModel vm) 
        {
            var userCookie = Request.Cookies["User"];
            var user = JsonConvert.DeserializeObject<User>(userCookie!);

            throw new InvalidOperationException("Neka greska");

            var allowedIngredients = vm.AllowedIngredients.Split(',').Select(s => s.Trim()).Select(s => s.ToLower()).ToList();
            var ingredients = vm.Ingredients.Split(',').Select(s => s.Trim()).Select(s => s.ToLower()).ToList();

            var disallowedWords = ingredients.Where(word => !allowedIngredients.Contains(word)).ToList();

            if (disallowedWords.Any())
            {
                ModelState.AddModelError("", "Uneli ste nedozvoljen sastojak: " + string.Join(", ", disallowedWords));
                return View("YourCustomPizza", vm);
            }

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

            _notyf.Success("Uspesno ste sacuvali picu!", 3);

            return RedirectToAction("Profile", "User");
        
        }

        public IActionResult MyPizzas() 
        {
            var userCookie = Request.Cookies["User"];
            var user = JsonConvert.DeserializeObject<User>(userCookie!);

            var vm = new PizzaManagerViewModel();
            vm.Pizzas = _pizzaRepository.GetUserPizzas(user.UserID);

            return View(vm);
        }

        public IActionResult EditPizza(int pizzaId) 
        {
            var pizza = _pizzaRepository.Pizzas.FirstOrDefault(p => p.ID == pizzaId);
            var vm = new UserCustomPizzaViewModel();
            vm.AllowedIngredients = "Sunka, Pecenica, Pelat, Sir, Masline, Pecurke, Jaje, Kukuruz, Paprika, Brokoli";

            return View(vm);
        }

        [HttpPost]
        public IActionResult EditPizza(UserCustomPizzaViewModel vm) 
        {
            var userCookie = Request.Cookies["User"];
            var user = JsonConvert.DeserializeObject<User>(userCookie!);

            var pizzaName = vm.PizzaName;

            var allowedIngredients = vm.AllowedIngredients.Split(',').Select(s => s.Trim()).Select(s => s.ToLower()).ToList();
            var ingredients = vm.Ingredients.Split(',').Select(s => s.Trim()).Select(s => s.ToLower()).ToList();

            var disallowedWords = ingredients.Where(word => !allowedIngredients.Contains(word)).ToList();

            if (disallowedWords.Any())
            {
                ModelState.AddModelError("", "Uneli ste nedozvoljen sastojak: " + string.Join(", ", disallowedWords));
                return View("YourCustomPizza", vm);
            }

            var pizza = new Pizza()
            {
                ID = vm.PizzaID,
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

            _pizzaRepository.EditPizza(pizza);

            _notyf.Success("Uspesno ste izmenili picu!", 3);

            return RedirectToAction("Profile", "User");

        }

        public IActionResult DeletePizza(int pizzaId) 
        {
            var pizza = _pizzaRepository.GetPizzaByID(pizzaId);
            _pizzaRepository.DeletePizza(pizza.ID);

            _notyf.Success("Uspesno ste obrisali picu!", 3);

            return RedirectToAction("Profile", "User");
        }

    }

}
