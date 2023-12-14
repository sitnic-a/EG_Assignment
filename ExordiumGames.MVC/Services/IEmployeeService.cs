namespace ExordiumGames.MVC.Services
{
    public interface IEmployeeService<TCategory,TItem, TRetailer>  where TCategory:class
                                                                   where TItem : class
                                                                   where TRetailer : class
    {
        Task<List<TItem>> GetItems();
        Task<TItem> GetItem(int id);

        Task<List<TCategory>> GetCategories();
        Task<TCategory> GetCategoryById(int id);
        Task<TCategory> AddAsyncCategory(TCategory Entity);
        Task<TCategory> DeleteAsyncCategory(int id);
        Task<TCategory> UpdateAsyncCategory(int id, TCategory Entity);

        Task<TItem> AddAsyncItem(TItem Entity);
        Task<TItem> DeleteAsyncItem(int id);
        Task<TItem> UpdateAsyncItem(int id, TItem Entity);

        Task<List<TRetailer>> GetRetailers();
        Task<TRetailer> GetRetailerById(int id);
        Task<TRetailer> AddAsyncRetailer(TRetailer Entity);
        Task<TRetailer> DeleteAsyncRetailer(int id);
        Task<TRetailer> UpdateAsyncRetailer(int id, TRetailer Entity);

        Task SaveChangesAsync();
    }
}
