using api.Dtos.Account;

namespace api.Services.Interfaces
{
    public interface IAccountService
    {
        Task<AccountRes> GetMyAccount();
        Task UpdateLogo(IFormFile formFile);
    }
}
