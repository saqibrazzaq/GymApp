using api.Dtos.Plan;
using api.Entities;
using api.Utility.Paging;

namespace api.Repository.Interfaces
{
    public interface ITimeUnitRepository : IRepositoryBase<TimeUnit>
    {
        PagedList<TimeUnit> Search(TimeUnitSearchReq dto, bool trackChanges);
    }
}
