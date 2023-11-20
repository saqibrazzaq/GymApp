using api.Common;
using api.Entities;
using api.Mailer;
using api.Models.Exceptions;
using api.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace api.Services.Implementations
{
    public class VerificationEmailSenderService : IVerificationEmailSenderService
    {
        private readonly UserManager<AppIdentityUser> _userManager;
        private readonly IEmailSender _emailSender;
        public VerificationEmailSenderService(UserManager<AppIdentityUser> userManager, 
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _emailSender = emailSender;
        }

        public async Task SendEmail(string email)
        {
            var userEntity = await _userManager.FindByEmailAsync(email);
            if (userEntity == null) throw new Exception("No user found with email " + email);

            // Check if email is already verified
            if (userEntity.EmailConfirmed == true)
                throw new BadRequestException("Email address already verified");

            // Create a token
            var pinCode = GeneratePinCode();
            var minutes = int.Parse(SecretUtility.JWTEmailVerificationTokenValidityInMinutes);

            // Update email verification token in repository
            userEntity.EmailVerificationToken = pinCode;
            userEntity.EmailVerificationTokenExpiryTime = DateTime.UtcNow.AddMinutes(minutes);
            await _userManager.UpdateAsync(userEntity);

            var emailVerificationText = GeneratePinCodeVerificationText(pinCode, minutes);
            _emailSender.SendEmail(userEntity.Email, "Email Verification",
                emailVerificationText);
        }

        private static string GeneratePinCode()
        {
            int _min = 1000;
            int _max = 9999;
            Random _rdm = new Random();
            return _rdm.Next(_min, _max).ToString();
        }

        private string GeneratePinCodeVerificationText(string pinCode, int minutes)
        {
            string text = $"Pin Code to verify your email address" +
                $"<br />{pinCode}<br />" +
                $"This pin code will expire in {minutes} minutes.";
            return text;
        }
    }
}
