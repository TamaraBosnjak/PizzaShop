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

        public bool IsExist(string username)
        {
            if (_appDBContext.Users.FirstOrDefault(u => u.UserName == username) != null)
            {
                return true;
            }

            return false;
        }
        public bool IsPasswordOK(string password) 
        {
            var inputPassword = EncryptionHelper.Encrypt(password);

            if(_appDBContext.Users.FirstOrDefault(u => u.Password == inputPassword) != null)
            {
                return true;
            }
            return false;
        }

    }
}
