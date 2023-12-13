using ExordiumGames.MVC.Data.DbModels;

namespace ExordiumGames.MVC.Services
{
    public interface IAdminService
    {
        Task<List<User>> GetUsers();
        Task<User> GetById(string userId);
        Task<List<User>> UpdateRoles(string userId);
        Task<User> DeleteById(string userId);
    }
}
