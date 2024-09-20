using EaseTrail.WebApp.Models.Enums;
using EaseTrail.WebApp.Models.Generic;

namespace EaseTrail.WebApp.Models
{
    public class WorkSpace : AllClasses
    {
        public WorkSpace() 
        {
            
        }

        public WorkSpace(Guid ownerId, string name, string description, string color, WorkSpaceStatus status)
        {
            OwnerId = ownerId;
            Name = name;
            Description = description;
            Color = color;
            Status = status;
            UserCount = 1;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public int UserCount { get; set; }
        public Guid OwnerId { get; set; }
        public Guid PlanId { get; set; }
        public WorkSpaceStatus Status { get; set; }

        public User Owner { get; set; }
        public Plan Plan { get; set; }

        public ICollection<UsersWorkSpace> UsersWorkSpaces { get; set; }
    }
}
