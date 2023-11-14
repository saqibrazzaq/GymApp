using api.Dtos.Plan;
using api.Dtos.User;
using api.Entities;
using api.Utility.Paging;

namespace api.Repository.Interfaces
{
    public interface IUserTypeRepository : IRepositoryBase<UserType>
    {
        PagedList<UserType> Search(UserTypeSearchReq dto, bool trackChanges);
    }
}
