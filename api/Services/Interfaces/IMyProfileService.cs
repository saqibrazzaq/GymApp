using api.Dtos.User;

namespace api.Services.Interfaces
{
    public interface IMyProfileService
    {
        Task SendVerificationEmail();
        Task VerifyEmail(VerifyEmailReq dto);
        Task ChangePassword(ChangePasswordReq dto);
        Task<AuthenticationRes> GetLoggedInUser();
        Task UpdateProfilePicture(IFormFile formFile);
    }
}
