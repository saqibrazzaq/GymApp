using api.Dtos.Country;
using api.Dtos.Plan;
using api.Entities;
using api.Utility.Paging;

namespace api.Services.Interfaces
{
    public interface IPlanTypeService
    {
        ApiOkPagedResponse<IEnumerable<PlanTypeRes>, MetaData> Search(PlanTypeSearchReq dto);
    }
}
