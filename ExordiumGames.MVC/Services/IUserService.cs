using ExordiumGames.MVC.Data.DbModels;

namespace ExordiumGames.MVC.Services
{
    public interface IUserService<TFitlerCategory, TFilterRetailer> where TFitlerCategory: class
                                                                    where TFilterRetailer : class
    {
        Task<IEnumerable<Category>> FilterCategoriesAsync(TFitlerCategory queryCategory);
        Task<IEnumerable<Retailer>> FilterRetailersAsync(TFilterRetailer queryRetailer);
    }
}
