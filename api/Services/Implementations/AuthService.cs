using api.Common;
using api.Dtos.User;
using api.Entities;
using api.Mailer;
using api.Models.Exceptions;
using api.Repository.Interfaces;
using api.Services.Interfaces;
using api.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace api.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppIdentityUser> _userManager;
        private readonly IRepositoryManager _repository;
        private readonly IEmailSender _emailSender;
        private readonly IVerificationEmailSenderService _verificationEmailSenderService;
        public AuthService(UserManager<AppIdentityUser> userManager,
            IRepositoryManager repository,
            IEmailSender emailSender,
            IVerificationEmailSenderService verificationEmailSenderService)
        {
            _userManager = userManager;
            _repository = repository;
            _emailSender = emailSender;
            _verificationEmailSenderService = verificationEmailSenderService;
        }

        public async Task<AuthenticationRes> Login(LoginReq dto)
        {
            // Authenticate user
            var userEntity = await AuthenticateUser(dto.Email, dto.Password);

            // If user/pwd are correct
            if (userEntity != null)
            {
                // Create response with access token
                var authRes = new AuthenticationRes
                {
                    Email = userEntity.Email,
                    Roles = await _userManager.GetRolesAsync(userEntity),
                    EmailConfirmed = userEntity.EmailConfirmed,
                    AccountId = userEntity.AccountId,
                    Id = userEntity.Id,
                    FullName = userEntity.FullName,
                    UserName = userEntity.UserName,
                    ProfilePictureUrl = userEntity.ProfilePictureUrl,
                };

                // Generate access/refresh tokens
                authRes.RefreshToken = GenerateRefreshToken();
                authRes.AccessToken = await GenerateAccessToken(userEntity);
                // Update user
                userEntity.RefreshToken = authRes.RefreshToken;
                userEntity.RefreshTokenExpiryTime = DateTime.UtcNow.AddMinutes(
                    int.Parse(SecretUtility.JWTRefreshTokenValidityInMinutes));
                await _userManager.UpdateAsync(userEntity);

                return authRes;
            }
            else throw new UnAuthorizedUserException("Incorrect username/password");
        }

        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private async Task<string> GenerateAccessToken(AppIdentityUser user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    //new Claim("userName", user.UserName),
                    //new Claim("email", user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    //new Claim("id", user.Id.ToString()),
                };

            string roles = "";
            foreach (var userRole in userRoles)
            {
                roles += userRole + ",";
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }
            authClaims.Add(new Claim("roles", roles));

            var token = CreateToken(authClaims);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private JwtSecurityToken CreateToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretUtility.JWTSecret));
            _ = int.TryParse(SecretUtility.JWTTokenValidityInMinutes, out int tokenValidityInMinutes);

            var token = new JwtSecurityToken(
                issuer: SecretUtility.JwtValidIssuer,
                audience: SecretUtility.JwtValidAudience,
                expires: DateTime.UtcNow.AddMinutes(tokenValidityInMinutes),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }

        private async Task<AppIdentityUser?> AuthenticateUser(string? email, string? password)
        {
            // Find user
            var userEntity = await _userManager.FindByEmailAsync(email);
            if (userEntity == null)
                return null;

            // Check password
            var isAuthenticated = await _userManager.CheckPasswordAsync(userEntity, password);

            return isAuthenticated ? userEntity : null;
        }

        public async Task<TokenRes> RefreshToken(TokenRes dto)
        {
            var principal = GetPrincipalFromExpiredToken(dto.AccessToken);
            if (principal == null || principal.Identity == null)
                throw new BadRequestException("Invalid access token");

            string? username = principal.Identity.Name;

            var userEntity = await _userManager.FindByNameAsync(username);

            if (userEntity == null || userEntity.RefreshToken != dto.RefreshToken
                || userEntity.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                throw new BadRequestException("Invalid refresh token");
            }

            var newAccessToken = await GenerateAccessToken(userEntity);
            return new TokenRes
            {
                //AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                AccessToken = newAccessToken,
                //RefreshToken = dto.RefreshToken,
            };
        }

        public async Task ResetPassword(ResetPasswordReq dto)
        {
            // Verify email address
            var userEntity = await _userManager.FindByEmailAsync(dto.Email);
            if (userEntity == null)
                throw new NotFoundException("Email address not found.");

            // Update password
            var result = await _userManager.ResetPasswordAsync(
                userEntity, dto.ForgotPasswordToken, dto.Password);

            if (result.Succeeded == false)
                throw new BadRequestException(AuthUtil.GetFirstErrorFromIdentityResult(
                    result, nameof(ResetPassword)));
        }

        private ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = SecretUtility.JwtValidIssuer,
                ValidAudience = SecretUtility.JwtValidAudience,
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretUtility.JWTSecret)),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                !jwtSecurityToken.Header.Alg.Equals(
                    SecurityAlgorithms.HmacSha256,
                    StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;

        }

        public async Task<AuthenticationRes> RegisterOwner(StaffCreateReq dto)
        {
            await CheckExistingEmail(dto.Email);

            // Create account
            var freeAccount = new Account { AccountTypeId = (int)AccountTypeNames.Free };
            _repository.AccountRepository.Create(freeAccount);
            _repository.Save();

            // Create the user
            var userEntity = new AppIdentityUser
            {
                UserName = dto.Email,
                Email = dto.Email,
                AccountId = freeAccount.AccountId
            };
            var result = await _userManager.CreateAsync(userEntity, dto.Password);

            if (result.Succeeded == false)
                throw new BadRequestException(AuthUtil.GetFirstErrorFromIdentityResult(
                    result, nameof(RegisterOwner)));

            // Add this user in Owner role
            var roleResult = await _userManager.AddToRoleAsync(
                userEntity, Constants.OwnerRole);
            if (roleResult.Succeeded == false)
                throw new BadRequestException(AuthUtil.GetFirstErrorFromIdentityResult(
                    roleResult, nameof(RegisterOwner)));

            await _verificationEmailSenderService.SendEmail(userEntity.Email);

            return await Login(new LoginReq { Email = dto.Email, Password = dto.Password });
        }

        private async Task CheckExistingEmail(string email)
        {
            // Email and username must not already exist
            var userEntity = await _userManager.FindByEmailAsync(email);
            if (userEntity != null)
                throw new BadRequestException($"Email {email} is already registered. Use Forgot password if you own this account.");
        }

        public async Task SendForgotPasswordEmail(SendForgotPasswordEmailReq dto)
        {
            // Verify email address
            var userEntity = await _userManager.FindByEmailAsync(dto.Email);
            if (userEntity == null)
                throw new NotFoundException("Email address not found.");

            // Create a token
            var forgotPasswordToken = await _userManager.GeneratePasswordResetTokenAsync(
                userEntity);
            // Generate forgot password email text
            string emailText = GenerateForgotPasswordEmailText(
                forgotPasswordToken);
            await _emailSender.SendEmail(userEntity.Email,
                "Reset your password", emailText);
        }

        private string GenerateForgotPasswordEmailText(
            string token)
        {
            string emailText = $"Please use the token below for password reset. <br />" +
                $"{token} <br />";
            return emailText;
        }
    }
}
