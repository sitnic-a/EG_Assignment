namespace ExordiumGames.MVC.Data.DbModels
{
    public class User
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Title { get; set; } = string.Empty;

        public User() { }
        public User(string email, string title)
        {
            UserName = email.ToUpperInvariant();
            Email = email;
            Title = title;
        }
    }
}
