using ExordiumGames.MVC.Data.DbModels;
using ExordiumGames.MVC.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExordiumGames.MVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnityController : ControllerBase
    {
        private readonly IEmployeeService<Category, Item, Retailer> _employeeService;

        public UnityController(IEmployeeService<Category,Item,Retailer> employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet("items")]
        public async Task<List<Item>> GetItemsUnityEndpoint()
        {
            var dbItems = await _employeeService.GetItems();
            return dbItems;
        }
    }
}
