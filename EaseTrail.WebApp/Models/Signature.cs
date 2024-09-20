using EaseTrail.WebApp.Models.Generic;

namespace EaseTrail.WebApp.Models
{
    public class Signature : AllClasses
    {
        public Guid PlanId { get; set; }
        public Guid BenefitId { get; set; }
        public Guid FeatureId { get; set; }

        public ICollection<Plan> Plans { get; set; }
        public ICollection<Benefit> Benefits { get; set; }
        public ICollection<Feature> Features { get; set; }
    }
}
