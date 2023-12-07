using api.Dtos.Plan;
using api.Dtos.Subscription;
using api.Entities;
using api.Utility.Paging;

namespace api.Repository.Interfaces
{
    public interface ISubscriptionRepository : IRepositoryBase<Subscription>
    {
        PagedList<Subscription> Search(
            SubscriptionSearchReq dto, bool trackChanges);
    }
}
