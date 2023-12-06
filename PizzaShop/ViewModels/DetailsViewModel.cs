using PizzaShop.Models;

namespace PizzaShop.ViewModels
{
    public class DetailsViewModel
    {
        public int ID { get; set; }
        public string ImageUrl { get; set; }
        public string LongDescription { get; set; }
        public decimal Price { get; set; }

        public DetailsViewModel(int id, string imageUrl, string longDescription, decimal price) 
        {
            ID = id;
            ImageUrl = imageUrl;
            LongDescription = longDescription;
            Price = price;
        }
    }
}
