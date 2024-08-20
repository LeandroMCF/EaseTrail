using System.ComponentModel.DataAnnotations.Schema;

namespace EaseTrail.WebApp.Inputs
{
    [NotMapped]
    public class UpdateUser
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string SecondName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string DocumentId { get; set; }
        public int Status { get; set; }
        public int UserType { get; set; }
    }
}
