namespace ExordiumGames.MVC.Dto
{
    public class UserDto
    {
        public string UserId { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public List<RoleDto> Roles { get; set; }

        public UserDto(){}

        public UserDto(string email, string title)
        {
            Email = email;
            Title = title;
            Username = email.ToUpperInvariant();
        }

        public UserDto(string userId, string email, string password, string title)
        {
            UserId = userId;
            Email = email;
            Password = password;
            Title = title;
            Username = email.ToUpperInvariant();
        }
    }
}
