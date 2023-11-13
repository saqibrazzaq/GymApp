using api.Dtos.Plan;
using api.Dtos.PlanCategory;
using api.Entities;
using api.Utility.Paging;

namespace api.Repository.Interfaces
{
    public interface IPlanRepository : IRepositoryBase<Plan>
    {
        PagedList<Plan> Search(
            PlanSearchReq dto, bool trackChanges);
    }
}
