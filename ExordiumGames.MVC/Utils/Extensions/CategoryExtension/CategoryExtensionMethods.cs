using ExordiumGames.MVC.Dto.FilteringDto;

namespace ExordiumGames.MVC.Utils.Extensions.CategoryExtension
{
    public static class CategoryExtensionMethods
    {
        public static bool CategoriesAreNotFiltered(this CategoryFilterDto categoryFilterDto)
        {
            return String.IsNullOrEmpty(categoryFilterDto.CategoryName) &&
                   String.IsNullOrWhiteSpace(categoryFilterDto.CategoryName) &&
                   String.IsNullOrEmpty(categoryFilterDto.ItemName) &&
                   String.IsNullOrWhiteSpace(categoryFilterDto.ItemName) &&
                   categoryFilterDto.CategoryPriority <= 0 &&
                   categoryFilterDto.ItemPrice <= 0;
                   
        }
    }
}
