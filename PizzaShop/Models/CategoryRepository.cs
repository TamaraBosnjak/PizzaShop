

namespace PizzaShop.Models
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDBContext _appDBContext;

        public CategoryRepository(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }
        public IEnumerable<Category> GetAllCategories()
        {
            return _appDBContext.Categories.ToList();
        }
        public Category GetCategoryByID(int? categoryId)
        {
            return _appDBContext.Categories.FirstOrDefault(c => c.ID == categoryId)!;
        }




    }
}
