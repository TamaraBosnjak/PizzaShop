using System.Collections.Generic;

namespace PizzaShop.Models
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> Categories { get; }
        Category GetCategoryByID(int id);
    }
}
