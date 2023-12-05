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

        public async Task<IActionResult> UpdateCategory(int CategoryId, Category category)
        {
            var dbCategory = await _employeeService.GetCategoryById(CategoryId);
            var items = _employeeService.GetItems().Result.Where(c => c.CategoryId != CategoryId);
            var categoryResponseModel = new CategoryResponseModel
            {
                Id = CategoryId,
                Name = dbCategory.Name,
                Priority = dbCategory.Priority,
            };
            foreach (var item in items)
            {
                categoryResponseModel.Items.Add(new CreateCategoryItemResponseModel
                {
                    ItemId = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    DiscountDate = item.DiscountDate,
                    ImageUrl = item.ImageUrl,
                    Price = item.Price,
                    CategoryId = item.CategoryId,
                    RetailerId = item.RetailerId,
                    CheckedItem=false
                });
            }

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

            return View(categoryResponseModel);
        }

        public IActionResult CreateCategory()
        {
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

        public async Task<IActionResult> SaveCategory(int CategoryId, CategoryResponseModel categoryRequestModel)
        {
            if (CategoryId > 0)
            {
                var updateCategory = new Category(categoryRequestModel.Name, categoryRequestModel.Priority);
                var updatedEntity = await _employeeService.UpdateAsyncCategory(CategoryId, updateCategory);

                foreach (var item in categoryRequestModel.Items.Where(c => c.CheckedItem == true))
                {
                    updateCategory.Items.Add(new Item(
                        item.ItemId,
                        item.Name,
                        item.Description,
                        item.DiscountDate,
                        item.ImageUrl,
                        item.Price,
                        item.RetailerId,
                        item.CategoryId));
                }
                foreach (var item in updateCategory.Items)
                {
                    var dbItem = await _employeeService.GetItem(item.Id);
                    dbItem.CategoryId = item.CategoryId;
                }
                await _employeeService.SaveChangesAsync();
                return RedirectToAction(actionName: "GetCategories");
            }
            var newCategory = new Category(categoryRequestModel.Name, categoryRequestModel.Priority);
            await _employeeService.AddAsyncCategory(newCategory);
            await _employeeService.SaveChangesAsync();
            return RedirectToAction(actionName: "GetCategories");
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
