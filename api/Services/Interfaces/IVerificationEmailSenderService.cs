namespace api.Services.Interfaces
{
    public interface IVerificationEmailSenderService
    {
        Task SendEmail(string email);
    }
}
