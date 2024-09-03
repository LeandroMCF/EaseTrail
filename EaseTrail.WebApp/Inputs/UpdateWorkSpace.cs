using EaseTrail.WebApp.Models.Enums;

namespace EaseTrail.WebApp.Inputs
{
    public class UpdateWorkSpace
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Color { get; set; } = "#F7643E";
        public int Status { get; set; }
    }
}
