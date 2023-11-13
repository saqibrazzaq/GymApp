using api.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using api.Dtos.PlanCategory;

namespace api.Dtos.Plan
{
    public class PlanEditReq
    {
        [Required, MinLength(3)]
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? PlanCategoryId { get; set; }
        public int? PlanTypeId { get; set; }
        public int SetupFee { get; set; } = 0;
        public int Price { get; set; } = 0;
    }
}
