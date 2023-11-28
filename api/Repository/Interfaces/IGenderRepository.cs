using api.Dtos.Plan;
using api.Dtos.User;
using api.Entities;
using api.Utility.Paging;

namespace api.Repository.Interfaces
{
    public interface IGenderRepository : IRepositoryBase<Gender>
    {
        PagedList<Gender> Search(GenderSearchReq dto, bool trackChanges);
    }
}
