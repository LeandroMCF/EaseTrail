using EaseTrail.WebApp.Models.Generic;

namespace EaseTrail.WebApp.Models
{
    public class Benefit : AllClasses
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Signature> Signatures { get; set; }
    }
}
