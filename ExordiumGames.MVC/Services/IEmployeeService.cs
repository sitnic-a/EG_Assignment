namespace ExordiumGames.MVC.Services
{
    public interface IEmployeeService<TCategory,TItem, TRetailer>  where TCategory:class
                                                                   where TItem : class
                                                                   where TRetailer : class
    {
        Task<List<TItem>> GetItems();
        Task<List<TCategory>> GetCategories();
        Task<List<TRetailer>> GetRetailers();
        //Task<TCategory> GetCategoryById();

        Task<TCategory> AddAsyncCategory(TCategory Entity);
        Task<TCategory> DeleteAsyncCategory(int id);
        Task<TCategory> UpdateAsyncCategory(int id, TCategory Entity);

        Task<TItem> AddAsyncItem(TItem Entity);
        Task<TItem> DeleteAsyncItem(int id);
        Task<TItem> UpdateAsyncItem(int id, TItem Entity);

        Task<TRetailer> AddAsyncRetailer(TRetailer Entity);
        Task<TRetailer> DeleteAsyncRetailer(int id);
        Task<TRetailer> UpdateAsyncRetailer(int id, TRetailer Entity);

        Task SaveChangesAsync();
    }
}
