namespace ExordiumGames.MVC.Dto
{
    public class RoleDto
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public bool CheckedItem { get; set; } = false;

        public RoleDto(){}
        public RoleDto(string id, string name)
        {
            Id= id;
            Name= name;
        }
    }
}
