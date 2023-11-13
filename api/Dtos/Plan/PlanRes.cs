using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using api.Dtos.PlanCategory;
using api.Entities;

namespace api.Dtos.Plan
{
    public class PlanRes
    {
        public int PlanId { get; set; }
        public int? AccountId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? PlanCategoryId { get; set; }
        public PlanCategoryRes? planCategory { get; set; }
        public int? PlanTypeId { get; set; }
        public PlanType? PlanType { get; set; }
    }
}
