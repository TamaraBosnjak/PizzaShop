using System.Collections;
using System.Collections.Generic;


namespace PizzaShop.Models
{
    public class CategoryRepository : ICategoryRepository
    {
        public IEnumerable<Category> Categories { get; }
         public Category GetCategoryById(int categoryId)
         {
             return null;
         }
        
    }
}
