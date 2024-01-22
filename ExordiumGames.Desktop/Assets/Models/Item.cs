using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Xml.Serialization;

namespace ExordiumGames.MVC.Data.DbModels
{
    public class Item
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string DiscountDate { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;
        public decimal Price { get; set; } = decimal.Zero;

        public string? RetailerName { get; set; } = string.Empty;

        public Retailer Retailer { get; set; }
        public int? RetailerId { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }

        public Item() { }

        public Item(string name, string description, string discountDate, string imageUrl, decimal price, int? retailerID, int categoryId)
        {
            Name = name;
            Description = description;
            DiscountDate = discountDate;
            ImageUrl = imageUrl;
            Price = price;
            RetailerId = retailerID;
            CategoryId = categoryId;
        }

        public Item(int itemId, string name, string description, string discountDate, string imageUrl, decimal price, int? retailerID, int categoryId)
        {
            Id = itemId;
            Name = name;
            Description = description;
            DiscountDate = discountDate;
            ImageUrl = imageUrl;
            Price = price;
            RetailerId = retailerID;
            CategoryId = categoryId;
        }

    }
}
