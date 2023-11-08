using System.Collections.Generic;

namespace PizzaShop.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public IEnumerable<Pie> Pies { get; set; }

        public Category(int categoryId, string categoryName, string description, IEnumerable<Pie> pies)
        {
            CategoryId = categoryId;
            CategoryName = categoryName;
            Description = description; 
            Pies= pies;
        }
    }
}
