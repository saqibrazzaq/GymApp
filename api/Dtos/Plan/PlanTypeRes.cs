using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Plan
{
    public class PlanTypeRes
    {
        public int PlanTypeId { get; set; }
        public string? Name { get; set; }
    }
}
