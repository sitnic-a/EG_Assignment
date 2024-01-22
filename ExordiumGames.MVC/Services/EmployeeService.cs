using ExordiumGames.MVC.Data;
using ExordiumGames.MVC.Data.DbModels;
using ExordiumGames.MVC.Dto.FilteringDto;
using ExordiumGames.MVC.Utils.Extensions.CategoryExtension;
using ExordiumGames.MVC.Utils.Extensions.RetailerExtension;
using Microsoft.EntityFrameworkCore;

namespace ExordiumGames.MVC.Services
{
    public class EmployeeService : IEmployeeService<Category, Item, Retailer>
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService<CategoryFilterDto, RetailerFilterDto> _userService;
        private readonly ILogger<EmployeeService> _employeeLogger;

        public EmployeeService(ApplicationDbContext context,
            IUserService<CategoryFilterDto, RetailerFilterDto> userService,
            ILogger<EmployeeService> employeeLogger)
        {
            _context = context;
            _userService = userService;
            _employeeLogger = employeeLogger;
        }

        public async Task<List<Item>> GetItems()
        {
            var result = await _context.Items.ToListAsync();
            return result;
        }

        public async Task<List<Category>> GetCategories(CategoryFilterDto queryCategory)
        {
            if (queryCategory != null)
            {
                if (queryCategory.CategoriesAreNotFiltered() == true)
                {
                    var result = await _context.Categories.Where(n => !String.IsNullOrEmpty(n.Name)).ToListAsync();
                    return result;
                }
            }
            var filteredCategories = await _userService.FilterCategoriesAsync(queryCategory);
            return filteredCategories.ToList();
        }

        public async Task<Category> GetCategoryById(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            return category != null ? category : new Category();
        }

        public async Task<List<Retailer>> GetRetailers(RetailerFilterDto? queryRetailer = null)
        {
            if (queryRetailer != null)
            {
                if (queryRetailer.RetailersAreNotFiltered() == true)
                {
                    var result = await _context.Retailers.Where(n => !String.IsNullOrEmpty(n.Name)).ToListAsync();
                    return result;
                }
            }
            var filteredCategories = await _userService.FilterRetailersAsync(queryRetailer);
            return filteredCategories.ToList();
        }

        public async Task<Category> AddAsyncCategory(Category Entity)
        {
            var newEntity = await _context.Categories.AddAsync(Entity);
            await _context.SaveChangesAsync();
            return newEntity.Entity;
        }

        public async Task<Item> AddAsyncItem(Item Entity)
        {
            var item = new Item(Entity.Name, Entity.Description, Entity.DiscountDate, Entity.ImageUrl, Entity.Price, Entity.RetailerId, Entity.CategoryId);
            var newEntity = await _context.Items.AddAsync(item);
            await _context.SaveChangesAsync();
            return newEntity.Entity;
        }

        public async Task<Retailer> AddAsyncRetailer(Retailer Entity)
        {
            var newEntity = await _context.Retailers.AddAsync(Entity);
            await _context.SaveChangesAsync();
            return newEntity.Entity;
        }

        public async Task<Category> DeleteAsyncCategory(int id)
        {
            var dbEntity = await _context.Categories.FindAsync(id);
            if (dbEntity is not null)
            {
                _context.Categories.Remove(dbEntity);
                await _context.SaveChangesAsync();
                return dbEntity;
            }
            return new Category();
        }

        public async Task<Item> DeleteAsyncItem(int id)
        {
            var dbEntity = await _context.Items.FindAsync(id);
            if (dbEntity is not null)
            {
                _context.Items.Remove(dbEntity);
                await _context.SaveChangesAsync();
                return dbEntity;
            }
            return new Item();
        }

        public async Task<Retailer> DeleteAsyncRetailer(int id)
        {
            var dbEntity = await _context.Retailers.FindAsync(id);
            if (dbEntity is not null)
            {
                foreach (var item in _context.Items.Where(i => i.RetailerId == id))
                {
                    _context.Items.Remove(item);
                }
                _context.Retailers.Remove(dbEntity);
                await _context.SaveChangesAsync();
                return dbEntity;
            }
            return new Retailer();
        }



        public async Task<Category> UpdateAsyncCategory(int id, Category Entity)
        {
            var dbEntity = await _context.Categories.FindAsync(id);
            //Entity Extension Method is not null to implement
            if (dbEntity is not null && Entity is not null)
            {
                dbEntity.Id = id;
                dbEntity.Name = Entity.Name;
                dbEntity.Priority = Entity.Priority;
                foreach (var item in Entity.Items)
                {
                    dbEntity.Items.Add(
                        new Item
                        {
                            Id = item.Id,
                            Name = item.Name,
                            Description = item.Description,
                            DiscountDate = item.DiscountDate,
                            ImageUrl = item.ImageUrl,
                            Price = item.Price,
                            CategoryId = item.CategoryId,
                            RetailerId = item.RetailerId
                        });
                }
                await _context.SaveChangesAsync();
                return dbEntity;
            }
            return new Category();
        }

        public async Task<Item> UpdateAsyncItem(int id, Item Entity)
        {
            var dbEntity = await _context.Items.FindAsync(id);
            //Extension method for Entity is not null to implement
            if (dbEntity is not null && Entity is not null)
            {
                dbEntity.Id = id;
                dbEntity.Name = Entity.Name;
                dbEntity.Description = Entity.Description;
                dbEntity.DiscountDate = Entity.DiscountDate;
                dbEntity.ImageUrl = Entity.ImageUrl;
                dbEntity.Price = Entity.Price;
                dbEntity.RetailerId = Entity.RetailerId;
                dbEntity.CategoryId = Entity.CategoryId;

                await _context.SaveChangesAsync();
                return dbEntity;
            }
            return new Item();
        }

        public async Task<Retailer> UpdateAsyncRetailer(int id, Retailer Entity)
        {
            var dbEntity = await _context.Retailers.FindAsync(id);
            //Extension method for Entity is not null to implement
            if (dbEntity is not null && Entity is not null)
            {
                dbEntity.Id = id;
                dbEntity.Name = Entity.Name;
                dbEntity.Priority = Entity.Priority;
                dbEntity.LogoImage = Entity.LogoImage;
                foreach (var item in Entity.Items)
                {
                    dbEntity.Items.Add(
                        new Item
                        {
                            Id = item.Id,
                            Name = item.Name,
                            Description = item.Description,
                            DiscountDate = item.DiscountDate,
                            ImageUrl = item.ImageUrl,
                            Price = item.Price,
                            CategoryId = item.CategoryId,
                            RetailerId = item.RetailerId
                        });
                }
                await _context.SaveChangesAsync();
                return dbEntity;
            }
            return new Retailer();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Item> GetItem(int id)
        {
            var dbItem = await _context.Items.FindAsync(id);
            return dbItem != null ? dbItem : new Item();
        }

        public async Task<Retailer> GetRetailerById(int id)
        {
            var retailer = await _context.Retailers.FindAsync(id);
            return retailer != null ? retailer : new Retailer();
        }
    }
}
