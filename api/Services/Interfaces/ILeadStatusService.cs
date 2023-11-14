using api.Dtos.Plan;
using api.Dtos.User;
using api.Utility.Paging;

namespace api.Services.Interfaces
{
    public interface ILeadStatusService
    {
        ApiOkPagedResponse<IEnumerable<LeadStatusRes>, MetaData> Search(LeadStatusSearchReq dto);
    }
}
