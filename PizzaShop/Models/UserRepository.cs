using Microsoft.EntityFrameworkCore;
using PizzaShop.Helpers;

namespace PizzaShop.Models
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDBContext _appDBContext;

        public UserRepository(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

        public void CreateUser(User user)
        {
            user.Password = EncryptionHelper.Encrypt(user.Password);

            _appDBContext.Users.Add(user);
            _appDBContext.SaveChanges();
        }

        public User GetUserByUsername(string username)
        {
            return _appDBContext.Users.FirstOrDefault(u => u.UserName == username)!;
        }


        public User GetUserByID(int ID)
        {
            return _appDBContext.Users.FirstOrDefault(u => u.UserID == ID)!;
        }

        public User GetUsersWithPizzasByUserID(int ID)
        {
            return _appDBContext.Users
                .Include(u => u.Orders)
                .ThenInclude(o => o.OrderDetails)
                .ThenInclude(od => od.Pizza)
                .FirstOrDefault(u => u.UserID == ID)!;
        }
        public void UpdatePassword(User user, string newPassword) 
        {
            user.Password = EncryptionHelper.Encrypt(newPassword);

            _appDBContext.Users.Update(user);

            _appDBContext.SaveChanges();
        }

    }
}
