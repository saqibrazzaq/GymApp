using api.Dtos.Plan;
using api.Utility.Paging;

namespace api.Services.Interfaces
{
    public interface ITimeUnitService
    {
        ApiOkPagedResponse<IEnumerable<TimeUnitRes>, MetaData> Search(TimeUnitSearchReq dto);
    }
}
