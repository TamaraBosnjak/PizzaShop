using System.Security.Claims;
using System.Security.Principal;

namespace PizzaShop.Models
{
    public class LoggedInUser : ClaimsIdentity, IIdentity
    {
        public string? Name {  get; set; }
        public string? AuthentificationType { get; set; }
        public bool IsAuthenticated {  get; set; }
    }
}
