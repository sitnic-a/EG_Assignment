using System.Text.Json.Serialization;

namespace ExordiumGames.MVC.Data.DbModels
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime DiscountDate { get; set; } = DateTime.Now.AddDays(10);
        public string ImageUrl { get; set; } = string.Empty;
        public decimal Price { get; set; } = decimal.Zero;
        public int RetailerId { get; set; }
        [JsonIgnore]
        public Retailer Retailer { get; set; }
        public int CategoryId { get; set; }
        [JsonIgnore]
        public Category Category { get; set; }

        public Item()
        {
            Retailer = new Retailer();
            Category = new Category();
        }

    }
}
