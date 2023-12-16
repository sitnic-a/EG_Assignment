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
        private readonly IEmployeeService<Category, Item, Retailer> _employeeService;
        private readonly ILogger<UserService> _userLogger;

        public UserService(ApplicationDbContext context,
            IEmployeeService<Category, Item, Retailer> employeeService,
            ILogger<UserService> userLogger)
        {
            _context = context;
            _employeeService = employeeService;
            _userLogger = userLogger;
        }
        public async Task<IEnumerable<Category>> FilterCategoriesAsync(CategoryFilterDto? queryCategory)
        {
            var query = _context.Categories
                .Include(i => i.Items);

            if (_context.Categories.Count() <= 0)
            {
                return new List<Category>();
            }

            if (queryCategory != null)
            {
                if (queryCategory.CategoriesAreNotFiltered() == true)
                {
                    return await _employeeService.GetCategories();
                }

                if (!String.IsNullOrEmpty(queryCategory.CategoryName) ||
                !String.IsNullOrWhiteSpace(queryCategory.CategoryName))
                {
                    query.Where(c => c.Name.StartsWith(queryCategory.CategoryName) ||
                                c.Name.EndsWith(queryCategory.CategoryName) ||
                                c.Name.Equals(queryCategory.CategoryName));
                }

                if (queryCategory.CategoryPriority > 0)
                {
                    query.Where(p => p.Priority.Equals(queryCategory.CategoryPriority));
                }

                if (!String.IsNullOrEmpty(queryCategory.ItemName) ||
                    !String.IsNullOrWhiteSpace(queryCategory.ItemName))
                {
                    query.SelectMany(i => i.Items)
                            .Where(i => i.Name.StartsWith(queryCategory.ItemName) ||
                                        i.Name.EndsWith(queryCategory.ItemName) ||
                                        i.Equals(queryCategory.ItemName));
                }

                if (queryCategory.ItemPrice > 0)
                {
                    query.SelectMany(i => i.Items)
                            .Where(p => p.Price.Equals(queryCategory.ItemPrice));
                }
                return query;
            }

            return await _employeeService.GetCategories();
        }

        public Task<IEnumerable<Retailer>> FilterRetailersAsync(RetailerFilterDto? queryRetailer)
        {
            throw new NotImplementedException();
        }
    }
}
