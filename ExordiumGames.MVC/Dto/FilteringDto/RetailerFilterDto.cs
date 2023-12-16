namespace ExordiumGames.MVC.Dto.FilteringDto
{
    public class RetailerFilterDto
    {
        public string RetailerName { get; set; } = string.Empty;
        public int Priority { get; set; }
        public string ItemName { get; set; } = string.Empty;
        public decimal ItemPrice { get; set; } = decimal.Zero;

        public RetailerFilterDto() { }
        public RetailerFilterDto(string retailerName, int priority, string itemName, decimal itemPrice)
        {
            RetailerName = retailerName;
            Priority = priority;
            ItemName = itemName;
            ItemPrice = itemPrice;
        }
    }
}
