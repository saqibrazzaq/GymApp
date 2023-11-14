using api.Dtos.Plan;
using api.Dtos.User;
using api.Entities;
using api.Utility.Paging;

namespace api.Repository.Interfaces
{
    public interface ILeadStatusRepository : IRepositoryBase<LeadStatus>
    {
        PagedList<LeadStatus> Search(LeadStatusSearchReq dto, bool trackChanges);
    }
}
