using ExordiumGames.MVC.Data.DbModels;

namespace ExordiumGames.MVC.Dto
{
    public class CreateCategoryItemResponseModel
    {
        public int ItemId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string DiscountDate { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public decimal Price { get; set; } = decimal.Zero;
        public int?RetailerId { get; set; }
        public int CategoryId { get; set; }
        public bool CheckedItem { get; set; }

        public CreateCategoryItemResponseModel() { }

        public CreateCategoryItemResponseModel(
            int itemId,
            string name, 
            string description, 
            string discountDate, 
            string imageUrl,
            decimal price, 
            int? retailerId, 
            int categoryId,
            bool checkedItem)
        {
            ItemId = itemId;
            Name = name;
            Description = description;
            DiscountDate = discountDate;
            ImageUrl = imageUrl;
            Price = price;
            RetailerId = retailerId;
            CategoryId = categoryId;
            CheckedItem = checkedItem;
        }

    }
}
