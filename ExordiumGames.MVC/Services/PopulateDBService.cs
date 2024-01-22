using ExordiumGames.MVC.Data;
using ExordiumGames.MVC.Data.DbModels;
using ExordiumGames.MVC.Utils.Parsers.XMLModels;
using ExordiumGames.MVC.Utils.Parsers;
using Newtonsoft.Json;

namespace ExordiumGames.MVC.Services
{
    public class PopulateDBService : IPopulateDBService
    {
        private readonly ApplicationDbContext _context;
        private int _categoriesNumber = 0;
        private int _itemsNumber = 0;
        private int _retailersNumber = 0;

        public PopulateDBService(ApplicationDbContext context)
        {
            _context = context;
            _categoriesNumber = _context.Categories.Count();
            _itemsNumber = _context.Items.Count();
            _retailersNumber = _context.Retailers.Count();
            PopulateData();
        }

        public void PopulateData()
        {
            XMLToJsonParser parser = new XMLToJsonParser();
            var jsonCategory = parser.Convert("Utils\\Parsers\\categories.xml");
            var jsonItems = parser.Convert("Utils\\Parsers\\items.xml");
            var jsonRetailers = parser.Convert("Utils\\Parsers\\retailers.xml");

            var dataCategories = JsonConvert.DeserializeObject<CategoriesXML>(jsonCategory);
            var dataItems = JsonConvert.DeserializeObject<ItemsXML>(jsonItems);
            var dataRetailers = JsonConvert.DeserializeObject<RetailersXML>(jsonRetailers);

            List<Category> categories = new List<Category>();
            if (dataCategories != null) categories = dataCategories.Categories.Category;

            List<Item> items = new List<Item>();
            if (dataItems != null) items = dataItems.Items.Item;

            List<Retailer> retailers = new List<Retailer>();
            if (dataRetailers != null) retailers = dataRetailers.Retailers.Retailer;

            if (_categoriesNumber == 0)
            {
                foreach (var category in categories)
                {
                    _context.Categories.Add(new Category
                    {
                        Name = category.Name,
                        Priority = category.Priority,
                    });
                }
            }

            if (_itemsNumber == 0)
            {
                foreach (var item in items)
                {
                    _context.Items.Add(item);
                }
            }

            if (_retailersNumber == 0)
            {
                foreach (var retailer in retailers)
                {
                    _context.Retailers.Add(new Retailer
                    {
                        Name = retailer.Name,
                        Priority = retailer.Priority,
                        LogoImage = retailer.LogoImage
                    });
                }
            }
            _context.SaveChanges();
        }
    }
}
