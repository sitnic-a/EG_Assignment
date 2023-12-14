namespace ExordiumGames.MVC.Dto
{
    public class RetailerRequestModel
    {
        public int RetailerId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Priority { get; set; }
        public string LogoImage { get; set; } = string.Empty;

        public List<CreateItemResponseModel> Items{ get; set; } = new List<CreateItemResponseModel>();
        public RetailerRequestModel() { }

        public RetailerRequestModel(int retailerId, string name, int priority, string logoImage)
        {
            RetailerId = retailerId;
            Name = name;
            Priority = priority;
            LogoImage = logoImage;
        }
    }
}
