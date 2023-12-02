using ExordiumGames.MVC.Data.DbModels;

namespace ExordiumGames.MVC.Dto
{
    public class CreateCategoryRequestModel
    {
        public string CategoryName { get; set; } = string.Empty;
        public int PrioriryValue { get; set; }
        public IList<CreateCategoryItemResponseModel> Items {get;set;} 

        public CreateCategoryRequestModel()
        {
           Items = new List<CreateCategoryItemResponseModel>();
        }
    }
}
