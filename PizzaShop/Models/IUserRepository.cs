namespace PizzaShop.Models
{
    public interface IUserRepository
    {
        void CreateUser(User user);
        bool IsExist(string username);
        bool IsPasswordOK(string password);
    }
}
