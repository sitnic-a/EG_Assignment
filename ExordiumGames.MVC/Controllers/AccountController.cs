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

        public AccountController(IEmployeeService<Category, Item, Retailer> employeeService)
        {
            _employeeService = employeeService;
        }

        public async Task<IActionResult> GetCategories()
        {
            var categories = await _employeeService.GetCategories();
            return View(categories);
        }

        public async Task<IActionResult> GetItems()
        {
            var items = await _employeeService.GetItems();
            return View(items);
        }

        public async Task<IActionResult> CategoryDetails(int CategoryId)
        {
            var category = await _employeeService.GetCategoryById(CategoryId);
            var categoryItems = _employeeService.GetItems().Result.Where(i => i.CategoryId == CategoryId).ToList();

            ViewBag.CategoryItems = categoryItems;

            return View(category);
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
        public async Task<IActionResult> CreateItem()
        {
            var categories = await _employeeService.GetCategories();
            var retailers = await _employeeService.GetRetailers();

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
        public IActionResult CreateRetailer()
        {
            return View();
        }

        public async Task<IActionResult> SaveCategory(CreateCategoryRequestModel categoryRequestModel)
        {
            var newCategory = new Category(categoryRequestModel.CategoryName, categoryRequestModel.PrioriryValue);
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
        public async Task<IActionResult> SaveItem(int ItemId, CreateItemRequestModel itemRequestModel)
        {
            if (ItemId <= 0)
            {
                var item = new Item(itemRequestModel.Name, itemRequestModel.Description, itemRequestModel.DiscountDate, itemRequestModel.ImageUrl, itemRequestModel.Price, itemRequestModel.RetailerId, itemRequestModel.CategoryId);
                var newEntity = await _employeeService.AddAsyncItem(item);
                return RedirectToAction(actionName: "GetItems");
            }
            var itemUpdate = new Item(itemRequestModel.Name, itemRequestModel.Description, itemRequestModel.DiscountDate, itemRequestModel.ImageUrl, itemRequestModel.Price, itemRequestModel.RetailerId, itemRequestModel.CategoryId);
            var updatedItem = await _employeeService.UpdateAsyncItem(ItemId, itemUpdate);
            return RedirectToAction(actionName: "GetItems");
        }
        public async Task<IActionResult> SaveRetailer(Retailer retailer)
        {
            var newEntity = await _employeeService.AddAsyncRetailer(retailer);
            return View(newEntity);
        }

        public async Task<IActionResult> DeleteItem(int ItemId)
        {
            var deletedItem = await _employeeService.DeleteAsyncItem(ItemId);
            return RedirectToAction(actionName: "GetItems");
        }

        public async Task<IActionResult> UpdateItem(int ItemId, Item item)
        {
            var dbItem = await _employeeService.GetItem(ItemId);
            var updateModel = new CreateItemRequestModel(
                ItemId,
                dbItem.Name,
                dbItem.Description,
                dbItem.DiscountDate,
                dbItem.ImageUrl,
                dbItem.Price,
                dbItem.RetailerId.Value,
                dbItem.CategoryId);

            var categories = await _employeeService.GetCategories();
            var retailers = await _employeeService.GetRetailers();

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

            return View(updateModel);
        }

    }
}
