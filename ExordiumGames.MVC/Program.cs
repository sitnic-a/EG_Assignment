using ExordiumGames.MVC.Data;
using ExordiumGames.MVC.Data.DbModels;
using ExordiumGames.MVC.Services;
using ExordiumGames.MVC.Utils.Parsers;
using ExordiumGames.MVC.Utils.Parsers.XMLModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//Custom services
builder.Services.AddTransient<IEmployeeService<Category, Item, Retailer>, EmployeeService>();
builder.Services.AddTransient<IPopulateDBService, PopulateDBService>();

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddMvc();

builder.Services.AddControllersWithViews();

var app = builder.Build();

var _context = builder.Services.BuildServiceProvider().GetRequiredService<ApplicationDbContext>();
PopulateDBService populateDB = new PopulateDBService(_context);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();





/* 1) Seed roles

   2) Seed one user - Admin

   3) Add additional tables : Retailers, Categories and Items

   4) - User controller
            - Read and write Retailer and Category filters set in Unity application

   5) - Employee
            - Add/remove/update (CRUD) of data:
            - Retailer
            - Category
            - Items
            - Everything else the User role scan
   ) - User
            - Read and write Retailer and Category filters set in Unity application
*/


app.Run();
