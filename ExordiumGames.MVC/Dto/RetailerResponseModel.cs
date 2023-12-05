namespace ExordiumGames.MVC.Dto
{
    public class RetailerResponseModel
    {
        public int RetailerId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Priority { get; set; }
        public List<CreateItemRequestModel> Items { get; set; } = new List<CreateItemRequestModel>();
        
        public RetailerResponseModel() { }
        public RetailerResponseModel(int retailerId, string name, int priority)
        {
            RetailerId = retailerId;
            Name = name;
            Priority = priority;
        }
    }
}
