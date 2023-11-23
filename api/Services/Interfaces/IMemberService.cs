using api.Dtos.User;
using api.Utility.Paging;

namespace api.Services.Interfaces
{
    public interface IMemberService : IUserService
    {
        Task CreateMember(StaffCreateReq dto);
        Task<ApiOkPagedResponse<IList<UserRes>, MetaData>>
            SearchMembers(SearchUsersReq dto, bool trackChanges);
    }
}