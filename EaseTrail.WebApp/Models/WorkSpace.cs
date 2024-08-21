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
        }

        public Guid OwnerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public WorkSpaceStatus Status { get; set; }

        public User Owner { get; set; }
    }
}
