using api.Data;
using api.Dtos.Plan;
using api.Entities;
using api.Repository.Interfaces;
using api.Utility.Paging;

namespace api.Repository.Implementations
{
    public class PlanTypeRepository : RepositoryBase<PlanType>, IPlanTypeRepository
    {
        public PlanTypeRepository(AppDbContext context) : base(context)
        {
        }

        public PagedList<PlanType> Search(PlanTypeSearchReq dto, bool trackChanges)
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
            return new PagedList<PlanType>(entities, count,
                dto.PageNumber, dto.PageSize);
        }
    }
}
