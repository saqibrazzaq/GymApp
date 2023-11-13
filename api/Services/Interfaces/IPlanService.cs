using api.Dtos.Plan;
using api.Dtos.PlanCategory;
using api.Utility.Paging;

namespace api.Services.Interfaces
{
    public interface IPlanService
    {
        Task<PlanRes> Create(PlanEditReq dto);
        Task<PlanRes> Update(int planId, PlanEditReq dto);
        Task Delete(int planId);
        Task<PlanRes> Get(int planId);
        Task<ApiOkPagedResponse<IEnumerable<PlanRes>, MetaData>> Search(
            PlanSearchReq dto);
    }
}
