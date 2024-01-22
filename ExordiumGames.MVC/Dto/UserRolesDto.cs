using Microsoft.AspNetCore.Identity;

namespace ExordiumGames.MVC.Dto
{
    public class UserRolesDto
    {
        public UserDto User { get; set; } = new UserDto();
        public IList<string> Roles { get; set; } = new List<string>();

        public UserRolesDto(){}

    }
}
