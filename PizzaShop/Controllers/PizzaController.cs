using Microsoft.AspNetCore.Mvc;
using PizzaShop.Models;
using PizzaShop.ViewModels;

namespace PizzaShop.Controllers
{
    public class PizzaController : Controller
    {
        private readonly IPizzaRepository _pizzaRepository;
        private readonly ICategoryRepository _categoryRepository;
        public PizzaController(IPizzaRepository pizzaRepository, ICategoryRepository categoryRepository)
        {
            _pizzaRepository = pizzaRepository;
            _categoryRepository = categoryRepository;
        }

        public IActionResult Index(bool uslov, bool uslov2)
        {
            ViewBag.Uslov = uslov;
            ViewBag.Uslov2 = uslov2;
            ViewBag.Message = "Ovo je server-side poruka.";
            ViewBag.Message2 = "Ovo je druga poruka.";

            return View();
        }
        //[Route("[controller]/Allpizzas/{categoryID?}")]
        public ViewResult List(int? categoryID)
        {
            IEnumerable<Pizza> pizzas;
            string? category = "Sve pice";
            if (categoryID > 0)
            { 
                pizzas = _pizzaRepository.Pizzas.Where(p => p.Category.ID == categoryID).OrderBy(p => p.ID);
                category = _categoryRepository.Categories.Where(c => c.ID == categoryID).Select(c => c.Name).FirstOrDefault();
            }
            else
            {
                pizzas = _pizzaRepository.Pizzas.OrderBy(p => p.ID);
            }

            return View(new PizzaListViewModel(pizzas, category));

        }
        public ViewResult Details(int id)
        {
            Pizza p = _pizzaRepository.GetPizzaByID(id);
            return View(new DetailsViewModel(id, p.ImageUrl, p.LongDescription, p.Price));
        }


    }
}
