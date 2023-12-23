using ExordiumGames.MVC.Dto.FilteringDto;

namespace ExordiumGames.MVC.Utils.Extensions.RetailerExtension
{
    public static class RetailerExtensionMethods
    {
        public static bool RetailersAreNotFiltered(this RetailerFilterDto retailerFilterDto)
        {
            return String.IsNullOrEmpty(retailerFilterDto.RetailerName) &&
                   String.IsNullOrWhiteSpace(retailerFilterDto.RetailerName) &&
                   String.IsNullOrEmpty(retailerFilterDto.ItemName) &&
                   String.IsNullOrWhiteSpace(retailerFilterDto.ItemName) &&
                   retailerFilterDto.RetailerPriority <= 0 &&
                   retailerFilterDto.ItemPrice <= 0;
        }
    }
}
