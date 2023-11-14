using api.Data;
using api.Dtos.User;
using api.Entities;
using api.Repository.Interfaces;
using api.Utility.Paging;

namespace api.Repository.Implementations
{
    public class LeadStatusRepository : RepositoryBase<LeadStatus>, ILeadStatusRepository
    {
        public LeadStatusRepository(AppDbContext context) : base(context)
        {
        }

        public PagedList<LeadStatus> Search(LeadStatusSearchReq dto, bool trackChanges)
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
            return new PagedList<LeadStatus>(entities, count,
                dto.PageNumber, dto.PageSize);
        }
    }
}
