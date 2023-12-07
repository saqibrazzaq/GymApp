using api.Dtos.Plan;
using api.Dtos.Subscription;
using api.Utility.Paging;

namespace api.Services.Interfaces
{
    public interface ISubscriptionService
    {
        Task<SubscriptionRes> Create(SubscriptionEditReq dto);
        Task<SubscriptionRes> Update(int subscriptionId, SubscriptionEditReq dto);
        Task Delete(int subscriptionId);
        Task<SubscriptionRes> Get(int subscriptionId);
        Task<ApiOkPagedResponse<IEnumerable<SubscriptionRes>, MetaData>> Search(
            SubscriptionSearchReq dto);
    }
}
