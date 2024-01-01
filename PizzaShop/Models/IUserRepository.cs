namespace PizzaShop.Models
{
    public interface IUserRepository
    {
        void CreateUser(User user);
        User GetUserByID(int iD);
        User GetUserByUsername(string username);
        void UpdatePassword(User user, string password);
        User GetUsersWithPizzasByUserID(int ID);
    }
}
