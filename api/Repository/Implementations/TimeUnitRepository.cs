using api.Data;
using api.Dtos.Plan;
using api.Entities;
using api.Repository.Interfaces;
using api.Utility.Paging;

namespace api.Repository.Implementations
{
    public class TimeUnitRepository : RepositoryBase<TimeUnit>, ITimeUnitRepository
    {
        public TimeUnitRepository(AppDbContext context) : base(context)
        {
        }

        public PagedList<TimeUnit> Search(TimeUnitSearchReq dto, bool trackChanges)
        {
            var entities = FindAll(trackChanges)
                .Search(dto)
                .Sort(dto.OrderBy)
                .Skip((dto.PageNumber - 1) * dto.PageSize)
                .Take(dto.PageSize)
                .ToList();
            var count = FindAll(trackChanges: false)
                .Search(dto)
                .Count();
            return new PagedList<TimeUnit>(entities, count,
                dto.PageNumber, dto.PageSize);
        }
    }
}
