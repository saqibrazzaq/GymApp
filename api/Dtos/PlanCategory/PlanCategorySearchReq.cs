using api.Utility.Paging;

namespace api.Dtos.PlanCategory
{
    public class PlanCategorySearchReq : PagedReq
    {
        public int? AccountId { get; set; } = 0;
    }
}
