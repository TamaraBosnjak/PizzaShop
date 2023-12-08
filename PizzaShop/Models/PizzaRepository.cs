using Microsoft.EntityFrameworkCore;

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
                return _appDBContext.Pizzas.Include(p => p.Category);
            }
        }
    }
}
