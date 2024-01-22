using System.Collections.Generic;

namespace ExordiumGames.MVC.Data.DbModels
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Priority { get; set; }
        public List<Item> Items { get; set; } = new List<Item>();

        public Category()
        {
            Items = new List<Item>();
        }

        public Category(string name, int priority)
        {
            Name = name;
            Priority = priority;
        }
    }
}
