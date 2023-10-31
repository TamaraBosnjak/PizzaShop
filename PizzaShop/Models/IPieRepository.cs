namespace PizzaShop.Models
{
    public interface IPieRepository
    {
        List<Pie> AllPies { get; set; }
        Pie GetPieById(int pieId);
    }
}
