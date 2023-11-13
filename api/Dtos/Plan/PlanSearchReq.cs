using api.Utility.Paging;

namespace api.Dtos.Plan
{
    public class PlanSearchReq : PagedReq
    {
        public int? AccountId { get; set; } = 0;
    }
}
