using ExordiumGames.MVC.Data;
using ExordiumGames.MVC.Data.DbModels;
using ExordiumGames.MVC.Dto.FilteringDto;
using ExordiumGames.MVC.Utils.Extensions.CategoryExtension;
using ExordiumGames.MVC.Utils.Extensions.RetailerExtension;
using Microsoft.EntityFrameworkCore;

namespace ExordiumGames.MVC.Services
{
    public class UserService : IUserService<CategoryFilterDto, RetailerFilterDto>
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UserService> _userLogger;
        private readonly IEnumerable<Category> _dbCategories;
        private readonly IEnumerable<Retailer> _dbRetailers;
        public UserService(ApplicationDbContext context,
            ILogger<UserService> userLogger)
        {
            _context = context;
            _userLogger = userLogger;
            _dbCategories = _context.Categories
                            .Where(c => !String.IsNullOrEmpty(c.Name) || !String.IsNullOrWhiteSpace(c.Name))
                            .Include(i => i.Items);
            _dbRetailers = _context.Retailers
                            .Where(r => !String.IsNullOrEmpty(r.Name) || !String.IsNullOrWhiteSpace(r.Name))
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

        public async Task<IEnumerable<Retailer>> FilterRetailersAsync(RetailerFilterDto? queryRetailer)
        {
            IEnumerable<Retailer> filteredItems = new List<Retailer>();
            List<Retailer> retailers = new List<Retailer>();

            if (_dbRetailers.Count() <= 0)
            {
                return new List<Retailer>();
            }

            if (queryRetailer != null)
            {
                if (queryRetailer.RetailersAreNotFiltered() == true)
                {
                    return _dbRetailers;
                }

                if (!String.IsNullOrEmpty(queryRetailer.RetailerName) ||
                !String.IsNullOrWhiteSpace(queryRetailer.RetailerName))
                {

                    filteredItems = _dbRetailers
                        .Where(c => c.Name.StartsWith(queryRetailer.RetailerName) ||
                                c.Name.EndsWith(queryRetailer.RetailerName) ||
                                c.Name.Equals(queryRetailer.RetailerName));
                    retailers.AddRange(filteredItems);
                }

                if (queryRetailer.RetailerPriority > 0)
                {
                    filteredItems = _dbRetailers
                        .Where(p => p.Priority == queryRetailer.RetailerPriority);
                    retailers.AddRange(filteredItems);
                }

                return retailers;
            }

            return _dbRetailers;
        }
    }
}
