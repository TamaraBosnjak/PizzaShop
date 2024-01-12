using Microsoft.AspNetCore.Mvc;
using PizzaShop.Models;

namespace PizzaShop.Components.CategoryMenu
{
    public class CategoryMenu : ViewComponent
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryMenu(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IViewComponentResult Invoke()
        {
            var userCookie = Request.Cookies["User"];
            var categories = _categoryRepository.GetAllCategories().OrderBy(c => c.Name).ToList();
            if (userCookie == null) 
            {
                var itemToRemove = categories.Single(r => r.Name == "Pice korisnika");
                categories.Remove(itemToRemove);
            }
            
            return View(categories);
        }
    }
}
