using System.Collections;
using System.Collections.Generic;
 

namespace PizzaShop.Models
{
    public class CategoryRepository : ICategoryRepository
    {
        private AppDBContext _appDBContext;

        public CategoryRepository(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }
        public IEnumerable<Category> Categories {
            get {  return _categories; }
        }
        public Category GetCategoryByID(int categoryId)
        {
            foreach (var category in _categories)
            {
                if (category.ID == categoryId)
                    return category;
            }
            return null;
        }
        private List<Category>_categories = new List<Category>();
        private List<Pizza> _pizzas = new List<Pizza>();

   

    }
}
