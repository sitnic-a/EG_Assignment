﻿namespace ExordiumGames.MVC.Dto
{
    public class CreateItemRequestModel
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string DiscountDate { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int RetailerId { get; set; }
        public int CategoryId { get; set; }

        public CreateItemRequestModel(){}
        public CreateItemRequestModel(string name, 
            string description, 
            string discountDate, 
            string imageUrl, 
            decimal price, 
            int retailerId, 
            int categoryId)
        {
            Name = name;
            Description = description;
            DiscountDate = discountDate;
            ImageUrl = imageUrl;
            Price = price;
            RetailerId = retailerId;
            CategoryId = categoryId;
        }

    }
}
