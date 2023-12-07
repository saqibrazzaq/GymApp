using api.Utility.Paging;

namespace api.Dtos.Subscription
{
    public class SubscriptionSearchReq : PagedReq
    {
        public int? AccountId { get; set; } = 0;
        public int? PlanId { get; set; } = 0;
        public string? Email { get; set; } = "";
    }
}
