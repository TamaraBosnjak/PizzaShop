namespace PizzaShop.Models
{
    public interface IUserRepository
    {
        void CreateUser(User user);
        User GetUserByUsername(string username);
    }
}
