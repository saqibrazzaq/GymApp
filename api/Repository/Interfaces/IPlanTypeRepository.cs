using api.Dtos.Currency;
using api.Dtos.Plan;
using api.Entities;
using api.Utility.Paging;

namespace api.Repository.Interfaces
{
    public interface IPlanTypeRepository : IRepositoryBase<PlanType>
    {
        PagedList<PlanType> Search(PlanTypeSearchReq dto, bool trackChanges);
    }
}
