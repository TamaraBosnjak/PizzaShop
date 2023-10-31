namespace PizzaShop.Models
{
    public class PieRepository : IPieRepository
    {
        public List<Pie>AllPies { 
            get { return _pies; }
            set { _pies = value; }
        }
        public Pie GetPieById(int pieId)
        { 
            foreach (var pie in _pies)
            {
                if (pie.PieId==pieId)
                return pie;
            }
            return null;
        }
        private List<Pie>_pies = new List<Pie>();

        public PieRepository()
        {
            Pie p1 = new Pie(1, "Pita s jabukama", "Mala pita sa jabukama", 1.25, false, true);
            Pie p2 = new Pie(1, "Makovnjača", "Velika štrudla sa makom", 8.35, true, true);
            Pie p3 = new Pie(1, "Burek", "Mastan burek sa sirom", 6.5, false, false);
            _pies.Add(p1);
            _pies.Add(p2); 
            _pies.Add(p3);
        }

        


    }
}
