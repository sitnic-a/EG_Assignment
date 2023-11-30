using ExordiumGames.MVC.Data.DbModels;
using ExordiumGames.MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExordiumGames.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IEmployeeService<Category, Item, Retailer> _employeeService;

        public AccountController(IEmployeeService<Category,Item,Retailer> employeeService)  
        {
            _employeeService = employeeService;
        }
        public IActionResult CreateCategory()
        {
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

        [HttpPost("addcategory")]
        public async Task<IActionResult> SaveCategory([FromBody]Category category)
        {
            var newEntity = await _employeeService.AddAsyncCategory(category);
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
