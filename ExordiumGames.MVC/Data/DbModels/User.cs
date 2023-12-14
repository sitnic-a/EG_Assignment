using Microsoft.AspNetCore.Identity;

namespace ExordiumGames.MVC.Data.DbModels
{
    public class User : IdentityUser
    {
        public string Title { get; set; } = string.Empty;

        public User(){}
        public User(string email, string title)
        {
            UserName = email.ToUpperInvariant();
            Email = email;
            Title = title;
        }
    }
}
