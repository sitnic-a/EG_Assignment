using ExordiumGames.MVC.Data.DbModels;
using ExordiumGames.MVC.Dto;
using ExordiumGames.MVC.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ExordiumGames.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IEmployeeService<Category, Item, Retailer> _employeeService;
        private readonly IAdminService _adminService;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(IEmployeeService<Category, Item, Retailer> employeeService,
            IAdminService adminService,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<User> signInManager)
        {
            _employeeService = employeeService;
            _adminService = adminService;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        #region Employee
        public IActionResult CreateCategory()
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
                    await _employeeService.UpdateAsyncItem(item.Id, new Item(item.Id,
                        item.Name,
                        item.Description,
                        item.DiscountDate,
                        item.ImageUrl,
                        item.Price,
                        item.RetailerId.Value,
                        item.CategoryId));

                    //var dbItem = await _employeeService.GetItem(item.Id);

                    //dbItem.CategoryId = item.CategoryId;
                }
                await _employeeService.SaveChangesAsync();
                return RedirectToAction(actionName: "GetCategories");
            }
            var newCategory = new Category(categoryRequestModel.Name, categoryRequestModel.Priority);
            await _employeeService.AddAsyncCategory(newCategory);
            await _employeeService.SaveChangesAsync();
            return RedirectToAction(actionName: "GetCategories");
        }

        public async Task<IActionResult> GetCategories()
        {
            var categories = await _employeeService.GetCategories();
            return View(categories);
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
                categoryResponseModel.Items.Add(new CreateItemResponseModel
                {
                    ItemId = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    DiscountDate = item.DiscountDate,
                    ImageUrl = item.ImageUrl,
                    Price = item.Price,
                    CategoryId = item.CategoryId,
                    RetailerId = item.RetailerId,
                    CheckedItem = false
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

        public async Task<IActionResult> DeleteCategory(int CategoryId)
        {
            var deletedCategory = await _employeeService.DeleteAsyncCategory(CategoryId);
            return RedirectToAction(actionName: "GetCategories");
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

        public async Task<IActionResult> SaveItem(int ItemId, CreateItemRequestModel itemRequestModel)
        {
            if (ItemId <= 0)
            {
                var item = new Item(itemRequestModel.Name, itemRequestModel.Description, itemRequestModel.DiscountDate, itemRequestModel.ImageUrl, itemRequestModel.Price, itemRequestModel.RetailerId, itemRequestModel.CategoryId);
                var newEntity = await _employeeService.AddAsyncItem(item);
                return RedirectToAction(actionName: "GetItems");
            }
            var itemUpdate = new Item(itemRequestModel.ItemId, itemRequestModel.Name, itemRequestModel.Description, itemRequestModel.DiscountDate, itemRequestModel.ImageUrl, itemRequestModel.Price, itemRequestModel.RetailerId, itemRequestModel.CategoryId);
            var updatedItem = await _employeeService.UpdateAsyncItem(ItemId, itemUpdate);
            return RedirectToAction(actionName: "GetItems");
        }

        public async Task<IActionResult> GetItems()
        {
            var items = await _employeeService.GetItems();
            return View(items);
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

        public async Task<IActionResult> DeleteItem(int ItemId)
        {
            var deletedItem = await _employeeService.DeleteAsyncItem(ItemId);
            return RedirectToAction(actionName: "GetItems");
        }


        public IActionResult CreateRetailer()
        {
            return View();
        }

        public async Task<IActionResult> SaveRetailer(int RetailerId, RetailerRequestModel retailerRequestModel)
        {
            if (RetailerId > 0)
            {
                var updateRetailer = new Retailer(retailerRequestModel.Name, retailerRequestModel.Priority, retailerRequestModel.LogoImage);
                var updatedEntity = await _employeeService.UpdateAsyncRetailer(RetailerId, updateRetailer);

                foreach (var item in retailerRequestModel.Items.Where(c => c.CheckedItem == true))
                {
                    updateRetailer.Items.Add(new Item(
                        item.ItemId,
                        item.Name,
                        item.Description,
                        item.DiscountDate,
                        item.ImageUrl,
                        item.Price,
                        item.RetailerId,
                        item.CategoryId));
                }
                foreach (var item in updateRetailer.Items)
                {
                    await _employeeService.UpdateAsyncItem(item.Id, new Item(item.Id,
                        item.Name,
                        item.Description,
                        item.DiscountDate,
                        item.ImageUrl,
                        item.Price,
                        item.RetailerId.Value,
                        item.CategoryId));
                }
                await _employeeService.SaveChangesAsync();
                return RedirectToAction(actionName: "GetRetailers");
            }
            var retailer = new Retailer(retailerRequestModel.Name, retailerRequestModel.Priority, retailerRequestModel.LogoImage);
            await _employeeService.AddAsyncRetailer(retailer);
            await _employeeService.SaveChangesAsync();
            return RedirectToAction(actionName: "GetRetailers");
        }

        public async Task<IActionResult> GetRetailers()
        {
            var retailers = await _employeeService.GetRetailers();
            return View(retailers);
        }

        public async Task<IActionResult> RetailerDetails(int RetailerId)
        {
            var retailer = await _employeeService.GetRetailerById(RetailerId);
            var retailerItems = _employeeService.GetItems().Result.Where(i => i.RetailerId == RetailerId);

            var retailerResponseModel = new RetailerRequestModel
            {
                RetailerId = RetailerId,
                Name = retailer.Name,
                Priority = retailer.Priority,
            };

            foreach (var item in retailerItems)
            {
                retailerResponseModel.Items.Add(new CreateItemResponseModel(
                    item.Id,
                    item.Name,
                    item.Description,
                    item.DiscountDate,
                    item.ImageUrl,
                    item.Price,
                    item.RetailerId.Value,
                    item.CategoryId,
                    false
                    ));
            }

            return View(retailerResponseModel);
        }

        public async Task<IActionResult> UpdateRetailer(int RetailerId)
        {
            var dbRetailer = await _employeeService.GetRetailerById(RetailerId);
            var items = _employeeService.GetItems().Result.Where(r => r.RetailerId != RetailerId);

            var retailerResponseModel = new RetailerRequestModel
            {
                RetailerId = RetailerId,
                Name = dbRetailer.Name,
                Priority = dbRetailer.Priority,
                LogoImage = dbRetailer.LogoImage
            };
            foreach (var item in items)
            {
                retailerResponseModel.Items.Add(new CreateItemResponseModel(
                    item.Id,
                    item.Name,
                    item.Description,
                    item.DiscountDate,
                    item.ImageUrl,
                    item.Price,
                    item.RetailerId.Value,
                    item.CategoryId,
                    false));
            }

            var retailers = await _employeeService.GetRetailers();
            var categories = await _employeeService.GetCategories();

            ViewBag.Retailers = retailers.Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Id.ToString()
            });
            ViewBag.Categories = categories.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });

            return View(retailerResponseModel);
        }

        public async Task<IActionResult> DeleteRetailer(int RetailerId)
        {
            var dbRetailer = await _employeeService.DeleteAsyncRetailer(RetailerId);
            return RedirectToAction(actionName: "GetRetailers");
        }

        #endregion

        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult CreateUser()
        {
            var roles = _roleManager.Roles.Select(r => new RoleDto { Id = r.Id, Name = r.Name }).ToList();
            UserDto user = new UserDto
            {
                Roles = roles
            };
            return View(user);
        }

        public async Task<IActionResult> SaveUser(UserDto registerUserDto)
        {
            var user = new User(registerUserDto.Email, registerUserDto.Title);
            var newUser = await _userManager.CreateAsync(user, registerUserDto.Password);
            foreach (var role in registerUserDto.Roles.Where(r => r.CheckedItem == true))
            {
                var wantedRole = await _roleManager.FindByIdAsync(role.Id);
                var inRole = await _userManager.IsInRoleAsync(user, wantedRole.Name);
                if (!inRole)
                {
                    var newRole = await _userManager.AddToRoleAsync(user, wantedRole.Name);
                }
            }
            return RedirectToAction(actionName: "Dashboard");
        }

        public async Task<IActionResult> GetUsers()
        {
            var dbUsers = await _adminService.GetUsers();
            List<UserDto> users = new List<UserDto>();
            UserRolesDto user;
            foreach (var dbUser in dbUsers)
            {
                var roles = await _userManager.GetRolesAsync(dbUser);
                user = new UserRolesDto
                {
                    User = new UserDto(dbUser.Email, dbUser.Title),
                    Roles = roles
                };
                users.Add(new UserDto
                {
                    UserId = dbUser.Id,
                    Email = user.User.Email,
                    Username = user.User.Username,
                    Title = user.User.Title,
                    Roles = user.Roles.Select(r => new RoleDto { Name = r }).ToList()
                });

            }
            return View(users);
        }

        public async Task<IActionResult> UserDetails(string UserId)
        {
            var dbUser = await _adminService.GetById(UserId);

            var dbUserInRoles = await _userManager.GetRolesAsync(dbUser);
            var roles = await _roleManager.Roles.Select(r => new RoleDto
            {
                Id = r.Id,
                Name = r.Name,
                CheckedItem = false
            }).ToListAsync();

            var userDto = new UserDto
            {
                UserId = dbUser.Id,
                Username = dbUser.UserName,
                Email = dbUser.Email,
                Title = dbUser.Title,
                Roles = roles
            };

            return View(userDto);
        }

        public async Task<IActionResult> UpdateUserRoles(string UserId, UserDto updateUserRoles)
        {
            var updatedRolesCount = updateUserRoles.Roles.Where(r => r.CheckedItem == true).Count();
            var user = await _adminService.GetById(UserId);
            var roles = await _userManager.GetRolesAsync(user);

            if (updatedRolesCount <= 0)
            {
                return RedirectToAction(actionName: "GetUsers");
            }
            
            foreach (var role in roles)
            {
                await _userManager.RemoveFromRoleAsync(user, role);
            }

            for (int i = 0; i < updatedRolesCount; i++)
            {
                var role = updateUserRoles.Roles[i].Name;
                await _userManager.AddToRoleAsync(user, role);
            }

            return RedirectToAction(actionName: "GetUsers");
        }

        public IActionResult Login()
        {
            return View();
        }

        public async Task<IActionResult> SignUserIn(UserDto loginUser)
        {
            var user = new User(loginUser.Email, loginUser.Title);
            var existingUser = _adminService.GetUsers().Result.FirstOrDefault(u => u.Email == loginUser.Email);
            if (existingUser != null)
            {
                if (await _userManager.CheckPasswordAsync(existingUser, loginUser.Password))
                {
                    await _signInManager.SignInAsync(existingUser, isPersistent: false);
                    return RedirectToAction(actionName: "Dashboard");
                }
            }

            return RedirectToAction("Login");
        }

        public async Task<IActionResult> SignUserOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(actionName: "Dashboard");
        }



        public async Task<IActionResult> DeleteUserById(string UserId)
        {
            var removedUser = await _adminService.DeleteById(UserId);
            return RedirectToAction(actionName: "GetUsers");
        }
    }
}
