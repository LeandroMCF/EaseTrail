namespace EaseTrail.WebApp.Outputs
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public List<WorkSpaceDto> WorkSpaces { get; set; }
    }
}
