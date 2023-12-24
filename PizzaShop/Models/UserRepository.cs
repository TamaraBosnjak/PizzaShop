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

    }
}
