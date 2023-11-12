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
        private List<Pie> _pies = new List<Pie>();

        public CategoryRepository()
        {
            Category c1 = new Category(1, "Pice sa mesom", "Opis za kategoriju 1",_pies);
            Category c2 = new Category(2, "Veganske pice", "Opis za kategoriju 2", _pies);
            Category c3 = new Category(3, "Pice bez glutena", "Opis za kategoriju 3",_pies);
            _categories.Add(c1);
            _categories.Add(c2);
            _categories.Add(c3);
            
            Pie p1 = new Pie(1, "Pita s jabukama", "Mala pita sa jabukama", 1.25, false, true, c2);
            Pie p2 = new Pie(2, "Makovnjača", "Velika štrudla sa makom", 8.35, true, true, c3);
            Pie p3 = new Pie(3, "Burek", "Mastan burek sa sirom", 6.5, false, false, c3);
            Pie p4 = new Pie(4, "Pita sa višnjama", "Pita sa višnjama bez šećera", 6.2, true, true, c2);
            Pie p5 = new Pie(5, "Pita sa junetinom", "Pita sa junećim mesom", 10.0, false, true, c1);
            Pie p6 = new Pie(6, "Pita sa pečurkama", "Mala pita sa pečurkama", 7.1, false, false, c2);
            Pie p7 = new Pie(7, "Pita zelje", "Pita sa sirom i zeljem", 6.66, true, true, c3);
            Pie p8 = new Pie(8, "Krompiruša", "Pita sa krompirom", 4.9, false, false, c2);
            Pie p9 = new Pie(9, "Pita sa sirom i dimnjenom slaninom", "Pita za gurmane", 5.55, false, true, c1);
            Pie p10 = new Pie(10, "Pita sa piletinom", "Pita sa pilećim mesom", 5.0, true, true, c1);
            _pies.Add(p1);
            _pies.Add(p2);
            _pies.Add(p3);
            _pies.Add(p4);
            _pies.Add(p5);
            _pies.Add(p6);
            _pies.Add(p7);
            _pies.Add(p8);
            _pies.Add(p9);
            _pies.Add(p10);
        }

    }
}
