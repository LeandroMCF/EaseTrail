using EaseTrail.WebApp.Models.Enums;
using EaseTrail.WebApp.Models.Generic;

namespace EaseTrail.WebApp.Models
{
    public class UsersWorkSpace : AllClasses
    {
        public UsersWorkSpace(Guid userId, Guid workSpaceId, ColaboratorType colaboratorType)
        {
            UserId = userId;
            WorkSpaceId = workSpaceId;
            ColaboratorType = colaboratorType;
        }

        public Guid UserId { get; set; }
        public Guid WorkSpaceId { get; set; }
        public ColaboratorType ColaboratorType { get; set; }

        public User User { get; set; }
        public WorkSpace WorkSpace { get; set; }
    }
}
