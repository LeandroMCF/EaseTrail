using System.ComponentModel.DataAnnotations.Schema;

namespace EaseTrail.WebApp.Inputs
{
    [NotMapped]
    public class Login
    {
        public string Email_UserName { get; set; }
        public string Password { get; set; }
    }
}
