using System.Collections;
using System.Collections.Generic;
 

namespace PizzaShop.Models
{
    public class CategoryRepository : ICategoryRepository
    {
        public IEnumerable<Category> Categories {
            get {  return _categories; }
        }
        public Category GetCategoryByID(int categoryId)
        {
            foreach(var category in _categories)
            {
                if(category.CategoryId == categoryId)
                return category;
            }
            return null;
        }
        private List<Category>_categories = new List<Category>();

        public CategoryRepository()
        {
            //Category c1 = new Category(1, "Pice sa mesom", "Opis za kategoriju 1");
            //Category c2 = new Category(2, "Veganske pice", "Opis za kategoriju 2");
            //Category c3 = new Category(3, "Pice bez glutena", "Opis za kategoriju 3");
            //_categories.Add(c1);
            //_categories.Add(c2);
            //_categories.Add(c3);
        }

    }
}
