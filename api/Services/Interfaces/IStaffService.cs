using api.Dtos.User;
using api.Utility.Paging;

namespace api.Services.Interfaces
{
    public interface IStaffService : IUserService
    {
        Task CreateStaff(StaffCreateReq dto);
        Task<ApiOkPagedResponse<IList<UserRes>, MetaData>>
            SearchStaff(SearchUsersReq dto, bool trackChanges);
        Task AddRoleToStaff(AddRoleReq dto);
        Task RemoveRoleFromStaff(RemoveRoleReq dto);
        IList<RoleRes> GetAllRoles();
    }
}