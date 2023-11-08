using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace api.Dtos.PlanCategory
{
    public class PlanCategoryEditReq
    {
        [Required, MinLength(3)]
        public string? Name { get; set; }
    }
}
