namespace PizzaShop.Models
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAllCategories();
        Category GetCategoryByID(int? categoryId);
    }
}
