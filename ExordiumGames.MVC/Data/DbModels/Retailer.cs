namespace ExordiumGames.MVC.Data.DbModels
{
    public class Retailer
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Priority { get; set; }
        public string LogoImage { get; set; } = string.Empty;
        public List<Item> Items { get; set; }

        public Retailer()
        {
            Items = new List<Item>();
        }
    }
}
