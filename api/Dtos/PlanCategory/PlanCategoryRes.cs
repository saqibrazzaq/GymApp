using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace api.Dtos.PlanCategory
{
    public class PlanCategoryRes
    {
        public int PlanCategoryId { get; set; }
        public int? AccountId { get; set; }
        public string? Name { get; set; }
    }
}
