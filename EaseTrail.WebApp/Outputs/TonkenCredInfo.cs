using System.ComponentModel.DataAnnotations.Schema;

namespace EaseTrail.WebApp.Outputs
{
    [NotMapped]
    public class TonkenCredInfo
    {
        public TonkenCredInfo(string id, int userType)
        {
            Id = id;
            UserType = userType;
        }

        public string Id { get; set; }
        public int UserType { get; set; }
    }
}
