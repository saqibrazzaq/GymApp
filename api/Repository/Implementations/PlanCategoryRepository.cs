using api.Data;
using api.Dtos.PlanCategory;
using api.Entities;
using api.Repository.Interfaces;
using api.Utility.Paging;

namespace api.Repository.Implementations
{
    public class PlanCategoryRepository : RepositoryBase<PlanCategory>, IPlanCategoryRepository
    {

        public PlanCategoryRepository(AppDbContext context) : base(context)
        {
        }

        public PagedList<PlanCategory> Search(PlanCategorySearchReq dto, bool trackChanges)
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
            return new PagedList<PlanCategory>(entities, count,
                dto.PageNumber, dto.PageSize);
        }
    }
}
