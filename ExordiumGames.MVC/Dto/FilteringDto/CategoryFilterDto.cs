namespace ExordiumGames.MVC.Dto.FilteringDto
{
    public class CategoryFilterDto
    {
        public string CategoryName { get; set; } = string.Empty;
        public int CategoryPriority { get; set; }
        public string ItemName { get; set; }= string.Empty;
        public decimal ItemPrice { get; set; }= decimal.Zero;

        public CategoryFilterDto(){}
        public CategoryFilterDto(string categoryName, int priority, string itemName, decimal itemPrice)
        {
            CategoryName = categoryName;
            CategoryPriority = priority;
            ItemName = itemName;
            ItemPrice = itemPrice;
        }

    }
}
