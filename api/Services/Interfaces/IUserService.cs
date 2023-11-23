using api.Dtos.User;
using api.Utility.Paging;

namespace api.Services.Interfaces
{
    public interface IUserService
    {
        Task Delete(DeleteUserReq dto);
        Task<UserRes> FindByUsername(string username);
        Task UpdateUser(string email, UserEditReq dto);
        Task UpdateProfilePicture(string email, IFormFile formFile);
        Task SetNewPassword(string email, SetNewPasswordReq dto);
    }
}