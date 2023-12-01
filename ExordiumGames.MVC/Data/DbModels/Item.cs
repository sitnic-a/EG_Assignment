using Newtonsoft.Json;
using System.Xml.Serialization;

namespace ExordiumGames.MVC.Data.DbModels
{
    public class Item
    {
        public int Id { get; set; }

        [JsonProperty("Nazivproizvoda")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty("Opis")]        
        public string Description { get; set; } = string.Empty;
        
        [JsonProperty("Datumakcije")]
        public string DiscountDate { get; set; }= string.Empty;
        
        [JsonProperty("URLdoslike")]
        public string ImageUrl { get; set; } = string.Empty;
        [JsonProperty("Cijena")]
        public decimal Price { get; set; } = decimal.Zero;
        
        [JsonProperty("Nazivretailera")]
        public string RetailerName { get; set; }=string.Empty;
        
        public int RetailerId { get; set; }
        
        [JsonIgnore]
        public Retailer Retailer { get; set; }
        
        [JsonProperty("Kategorija")]
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
