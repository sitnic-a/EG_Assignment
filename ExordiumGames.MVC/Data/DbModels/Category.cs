namespace ExordiumGames.MVC.Data.DbModels
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Priority { get; set; }
        public List<Item> Items { get; set; }

        public Category()
        {
            Items = new List<Item>();
        }
    }
}
