using api.Dtos.User;

namespace api.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthenticationRes> Login(LoginReq dto);
        Task<AuthenticationRes> RegisterOwner(StaffCreateReq dto);
        Task<TokenRes> RefreshToken(TokenRes dto);
        Task SendForgotPasswordEmail(SendForgotPasswordEmailReq dto);
        Task ResetPassword(ResetPasswordReq dto);
    }
}
