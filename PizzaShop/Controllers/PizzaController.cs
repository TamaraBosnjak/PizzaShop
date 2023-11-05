using Microsoft.AspNetCore.Mvc;
using PizzaShop.Models;

namespace PizzaShop.Controllers
{
    public class PizzaController : Controller
    {
        private IPieRepository _repository;
        public PizzaController(IPieRepository repository)
        {
            _repository = repository;
        }
        public IActionResult Index(bool uslov, bool uslov2)
        {
            ViewBag.Uslov = uslov;
            ViewBag.Uslov2 = uslov2;
            ViewBag.Message = "Ovo je server-side poruka.";
            ViewBag.Message2 = "Ovo je druga poruka.";
          
            return View();
        }
        public ViewResult List()
        {
            return View(_repository.AllPies);
        }
    }
}
