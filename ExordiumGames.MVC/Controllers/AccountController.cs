using ExordiumGames.MVC.Data.DbModels;
using ExordiumGames.MVC.Dto;
using ExordiumGames.MVC.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ExordiumGames.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IEmployeeService<Category, Item, Retailer> _employeeService;

        public AccountController(IEmployeeService<Category,Item,Retailer> employeeService)  
        {
            _employeeService = employeeService;
        }
        public async Task<IActionResult> CreateCategory()
        {
            var items = await _employeeService.GetItems();
            var categories = await _employeeService.GetCategories();
            var retailers = await _employeeService.GetRetailers();

            IList<CreateCategoryItemResponseModel> itemsResponseModel = new List<CreateCategoryItemResponseModel>();
            foreach (var item in items)
            {
                itemsResponseModel.Add(new CreateCategoryItemResponseModel(item.Id,
                    item.Name, 
                    item.Description, 
                    item.DiscountDate, 
                    item.ImageUrl,
                    item.Price, 
                    item.RetailerId,
                    item.CategoryId, 
                    false));
            }

            ViewBag.Items = itemsResponseModel;
            ViewBag.Categories = categories.Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            }).ToList();
            ViewBag.Retailers = retailers.Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            }).ToList();
            
            return View();
        }
        public IActionResult CreateItem()
        {
            return View();
        }
        public IActionResult CreateRetailer()
        {
            return View();
        }

        public async Task<IActionResult> SaveCategory(CreateCategoryRequestModel categoryRequestModel)
        {
            var newCategory = new Category(categoryRequestModel.CategoryName,categoryRequestModel.PrioriryValue);
            var newEntity = await _employeeService.AddAsyncCategory(newCategory);

            foreach (var item in categoryRequestModel.Items.Where(c => c.CheckedItem == true))
            {
                newCategory.Items.Add(new Item(
                    item.ItemId,
                    item.Name,
                    item.Description,
                    item.DiscountDate,
                    item.ImageUrl,
                    item.Price,
                    item.RetailerId,
                    item.CategoryId));
            }
            await _employeeService.SaveChangesAsync();
            return View(newEntity);
        }
        public async Task<IActionResult> SaveItem(Item item)
        {
            var newEntity = await _employeeService.AddAsyncItem(item);
            return View(newEntity);
        }
        public async Task<IActionResult> SaveRetailer(Retailer retailer)
        {
            var newEntity = await _employeeService.AddAsyncRetailer(retailer);
            return View(newEntity);
        }

    }
}
