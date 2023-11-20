using api.Dtos.User;
using api.Utility.Paging;

namespace api.Services.Interfaces
{
    public interface IUserService
    {
        Task CreateStaff(StaffCreateReq dto);
        Task Delete(DeleteUserReq dto);
        Task<ApiOkPagedResponse<IList<UserRes>, MetaData>>
            SearchUsers(SearchUsersReq dto, bool trackChanges);
        Task<UserRes> FindByUsername(string username);
        Task AddRoleToUser(AddRoleReq dto);
        Task RemoveRoleFromUser(RemoveRoleReq dto);
        IList<RoleRes> GetAllRoles();
        Task UpdateStaff(string email, StaffEditReq dto);
    }
}