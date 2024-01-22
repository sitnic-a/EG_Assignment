namespace ExordiumGames.MVC.Dto.FilteringDto
{
    public class RetailerFilterDto
    {
        public string RetailerName { get; set; } = string.Empty;
        public int RetailerPriority { get; set; }
        public string ItemName { get; set; } = string.Empty;
        public decimal ItemPrice { get; set; } = decimal.Zero;

        public RetailerFilterDto() { }
        public RetailerFilterDto(string retailerName, int priority, string itemName, decimal itemPrice)
        {
            RetailerName = retailerName;
            RetailerPriority = priority;
            ItemName = itemName;
            ItemPrice = itemPrice;
        }
    }
}
