using Microsoft.AspNetCore.Identity;

namespace ExordiumGames.MVC.Data
{
    public class User : IdentityUser
    {
        public string Title { get; set; }
    }
}
