namespace ExordiumGames.MVC.Dto
{
    public class UserDto
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public List<RoleDto> Roles { get; set; }

        public UserDto(){}

        public UserDto(string email, string password, string title)
        {
            Email = email;
            Password = password;
            Title = title;
        }
    }
}
