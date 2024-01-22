using ExordiumGames.MVC.Data.DbModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Models.SerializationModels
{
    public class JsonItems
    {
        [JsonProperty("")]
        public List<Item> items { get;set; }
    }
}
