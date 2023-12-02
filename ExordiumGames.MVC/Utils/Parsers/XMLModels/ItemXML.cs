using ExordiumGames.MVC.Data.DbModels;
using System.Xml.Serialization;

namespace ExordiumGames.MVC.Utils.Parsers.XMLModels
{
    public class ItemXML
    {
        public List<Item> Item { get; set; } = new List<Item>();
    }
}
