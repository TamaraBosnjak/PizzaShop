using Microsoft.EntityFrameworkCore;
using PizzaShop.ViewModels;

namespace PizzaShop.Models
{
    public class PizzaRepository : IPizzaRepository
    {
        private AppDBContext _appDBContext;
        public PizzaRepository(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;

        }
        public Pizza GetPizzaByID(int id)
        {
            return _appDBContext.Pizzas.Include(p => p.Category).FirstOrDefault(p => p.ID == id);
        }
        public IEnumerable<Pizza> Pizzas
        {
            get
            {
                return _appDBContext.Pizzas.Include(p => p.Category).Where(c => c.Category.ID != 4);
            }
        }
        public Pizza AddUsersCustomPizza(int userID)
        {
            return _appDBContext.Pizzas.Include(p => p.Category).FirstOrDefault(c => c.UserID == userID);

            _appDBContext.Pizzas.Add();
            _appDBContext.SaveChanges();
        }
    }
}
