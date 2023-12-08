using ExordiumGames.MVC.Data;
using ExordiumGames.MVC.Data.DbModels;
using Microsoft.EntityFrameworkCore;

namespace ExordiumGames.MVC.Services
{
    public class AdminService : IAdminService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AdminService> _adminLogger;

        public AdminService(ApplicationDbContext context, ILogger<AdminService> adminLogger)
        {
            _context = context;
            _adminLogger = adminLogger;
        }
        public async Task<List<User>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }
    }
}
