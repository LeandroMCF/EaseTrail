using EaseTrail.WebApp.Models.Enums;
using EaseTrail.WebApp.Models.Generic;

namespace EaseTrail.WebApp.Models
{
    public class Invite : AllClasses
    {
        public string FromEmail { get; set; }
        public string ToEmail { get; set; }
        public bool ReciveUserExists { get; set; }
        public ColaboratorType ColaboratorType { get; set; }
        public Guid WorkSpaceId { get; set; }

        public WorkSpace WorkSpace { get; set; }
    }
}
