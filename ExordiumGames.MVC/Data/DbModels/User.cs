using Microsoft.AspNetCore.Identity;

namespace ExordiumGames.MVC.Data.DbModels
{
    public class User : IdentityUser
    {
        public string Title { get; set; }
    }
}
