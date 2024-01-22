using Newtonsoft.Json;

namespace ExordiumGames.MVC.Data.DbModels
{
    public class Retailer
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Priority { get; set; }
        [JsonProperty("LogoImageUrl")]
        public string LogoImage { get; set; } = string.Empty;
        public List<Item> Items { get; set; } = new List<Item>();

        public Retailer()
        {
            Items = new List<Item>();
        }

        public Retailer(string name, int priority, string logoImage)
        {
            Name= name;
            Priority= priority;
            LogoImage= logoImage;
        }
    }
}
