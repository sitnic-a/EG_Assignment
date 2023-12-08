using ExordiumGames.MVC.Data.DbModels;

namespace ExordiumGames.MVC.Services
{
    public interface IAdminService
    {
        Task<List<User>> GetUsers();
    }
}
