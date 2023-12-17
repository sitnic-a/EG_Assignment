using ExordiumGames.MVC.Data;
using ExordiumGames.MVC.Data.DbModels;
using ExordiumGames.MVC.Dto.FilteringDto;
using ExordiumGames.MVC.Utils.Extensions.CategoryExtension;
using Microsoft.EntityFrameworkCore;

namespace ExordiumGames.MVC.Services
{
    public class UserService : IUserService<CategoryFilterDto, RetailerFilterDto>
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UserService> _userLogger;
        private readonly IEnumerable<Category> _dbCategories;
        public UserService(ApplicationDbContext context,
            ILogger<UserService> userLogger)
        {
            _context = context;
            _userLogger = userLogger;
            _dbCategories = _context.Categories
                            .Where(c => !String.IsNullOrEmpty(c.Name) || !String.IsNullOrWhiteSpace(c.Name))
                            .Include(i => i.Items);
        }
        public async Task<IEnumerable<Category>> FilterCategoriesAsync(CategoryFilterDto? queryCategory)
        {
            IEnumerable<Category> filteredItems = new List<Category>();
            List<Category> categories = new List<Category>();

            if (_dbCategories.Count() <= 0)
            {
                return new List<Category>();
            }

            if (queryCategory != null)
            {
                if (queryCategory.CategoriesAreNotFiltered() == true)
                {
                    return _dbCategories;
                }

                if (!String.IsNullOrEmpty(queryCategory.CategoryName) ||
                !String.IsNullOrWhiteSpace(queryCategory.CategoryName))
                {

                    filteredItems = _dbCategories
                        .Where(c => c.Name.StartsWith(queryCategory.CategoryName) ||
                                c.Name.EndsWith(queryCategory.CategoryName) ||
                                c.Name.Equals(queryCategory.CategoryName));
                    categories.AddRange(filteredItems);
                }

                if (queryCategory.CategoryPriority > 0)
                {
                    filteredItems = _dbCategories
                        .Where(p => p.Priority == queryCategory.CategoryPriority);
                    categories.AddRange(filteredItems);
                }

                return categories;
            }

            return _dbCategories;
        }

        public Task<IEnumerable<Retailer>> FilterRetailersAsync(RetailerFilterDto? queryRetailer)
        {
            throw new NotImplementedException();
        }
    }
}
