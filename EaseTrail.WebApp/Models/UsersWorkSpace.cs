using EaseTrail.WebApp.Models.Enums;
using EaseTrail.WebApp.Models.Generic;

namespace EaseTrail.WebApp.Models
{
    public class UsersWorkSpace : AllClasses
    {
        public UsersWorkSpace(string userEmail, Guid workSpaceId, ColaboratorType colaboratorType)
        {
            UserEmail = userEmail;
            WorkSpaceId = workSpaceId;
            ColaboratorType = colaboratorType;
            InviteStatus = InviteStatus.Invite;
        }

        public string UserEmail { get; set; }
        public Guid WorkSpaceId { get; set; }
        public ColaboratorType ColaboratorType { get; set; }
        public InviteStatus InviteStatus { get; set; }

        public WorkSpace WorkSpace { get; set; }
    }
}
