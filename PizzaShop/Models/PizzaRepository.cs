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
            return _appDBContext.Pizzas.Include(p => p.Category).FirstOrDefault(p => p.ID == id)!;
        }

        public IEnumerable<Pizza> Pizzas
        {
            get
            {
                return _appDBContext.Pizzas.Include(p => p.Category);
            }
        }

        public void SavePizza(Pizza pizza) 
        {
            _appDBContext.Pizzas.Add(pizza);
            _appDBContext.SaveChanges();
        }

        public void DeletePizza(int id)
        {
            var cartItem = _appDBContext.Pizzas.FirstOrDefault(p => p.ID == id)!;
            _appDBContext.Pizzas.Remove(cartItem);
            _appDBContext.SaveChanges();
        }

        public List<Pizza> GetUserPizzas(int userID)
        {
            return _appDBContext.Pizzas.Where(p => p.UserID == userID).ToList();
        }
        public void EditPizza(Pizza pizza)
        {
            _appDBContext.Pizzas.Update(pizza);
            _appDBContext.SaveChanges();
        }
    }
}
