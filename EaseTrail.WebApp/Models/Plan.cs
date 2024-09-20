using EaseTrail.WebApp.Models.Generic;

namespace EaseTrail.WebApp.Models
{
    public class Plan : AllClasses
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Signature> Signatures { get; set; }
        public ICollection<WorkSpace> WorkSpaces { get; set; }
    }
}
