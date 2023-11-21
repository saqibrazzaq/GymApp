using api.Common;
using api.Dtos.User;
using api.Entities;
using api.Models.Exceptions;
using api.Services.Interfaces;
using api.Storage;
using api.Utility;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace api.Services.Implementations
{
    public class MyProfileService : IMyProfileService
    {
        private readonly UserManager<AppIdentityUser> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IMapper _mapper;
        private readonly IVerificationEmailSenderService _verificationEmailSenderService;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly IWebHostEnvironment _environment;
        public MyProfileService(UserManager<AppIdentityUser> userManager,
            IHttpContextAccessor contextAccessor,
            IMapper mapper,
            IVerificationEmailSenderService verificationEmailSenderService,
            ICloudinaryService cloudinaryService,
            IWebHostEnvironment environment)
        {
            _userManager = userManager;
            _contextAccessor = contextAccessor;
            _mapper = mapper;
            _verificationEmailSenderService = verificationEmailSenderService;
            _cloudinaryService = cloudinaryService;
            _environment = environment;
        }

        public async Task ChangePassword(ChangePasswordReq dto)
        {
            // Verify email address
            var userEntity = await _userManager.FindByEmailAsync(UserName);
            if (userEntity == null)
                throw new NotFoundException("No email address found " + UserName);

            // Reset password
            var result = await _userManager.ChangePasswordAsync(
                userEntity, dto.CurrentPassword, dto.NewPassword);

            if (result.Succeeded == false)
            {
                throw new BadRequestException(AuthUtil.GetFirstErrorFromIdentityResult(
                    result, nameof(ChangePassword)));
            }
        }

        public async Task<AuthenticationRes> GetLoggedInUser()
        {
            //_userManager.Get
            var userEntity = await _userManager.FindByNameAsync(UserName);
            if (userEntity == null)
                throw new NotFoundException("User not found");

            var userDto = _mapper.Map<AuthenticationRes>(userEntity);
            userDto.Roles = await _userManager.GetRolesAsync(userEntity);
            return userDto;
        }

        private string? UserName
        {
            get
            {
                if (_contextAccessor.HttpContext != null &&
                    _contextAccessor.HttpContext.User.Identity != null &&
                    string.IsNullOrWhiteSpace(_contextAccessor.HttpContext.User.Identity.Name) == false)
                    return _contextAccessor.HttpContext.User.Identity.Name;
                else
                    throw new UnauthorizedAccessException("User not logged in");
            }
        }

        public async Task SendVerificationEmail()
        {
            // Verify email address
            var userEntity = await _userManager.FindByNameAsync(UserName);
            if (userEntity == null)
                throw new NotFoundException("User not found.");

            await _verificationEmailSenderService.SendEmail(userEntity.Email);
        }

        public async Task UpdateProfilePicture(IFormFile file)
        {
            var uploadResult = _cloudinaryService.UploadProfilePictureThumbnail(file, TempFolderPath);
            await updateProfilePictureInRepository(uploadResult);
        }

        private async Task updateProfilePictureInRepository(CloudinaryUploadResultRes uploadResult)
        {
            var userEntity = await _userManager.FindByNameAsync(UserName);
            //DeleteExistingProfilePictureFromCloudinary(userEntity.ProfilePictureCloudinaryId);
            _cloudinaryService.DeleteImage(userEntity.ProfilePictureCloudinaryId);
            userEntity.ProfilePictureUrl = uploadResult.SecureUrl;
            userEntity.ProfilePictureCloudinaryId = uploadResult.PublicId;
            await _userManager.UpdateAsync(userEntity);
        }

        public string TempFolderPath
        {
            get
            {
                return Path.Combine(_environment.WebRootPath, Constants.TempFolderName);
            }
        }

        public async Task VerifyEmail(VerifyEmailReq dto)
        {
            // Verify email address
            var userEntity = await _userManager.FindByNameAsync(UserName);
            if (userEntity == null)
                throw new NotFoundException("Email address not found.");

            // Check if email is already verified
            if (userEntity.EmailConfirmed == true)
                throw new BadRequestException("Email address already verified");

            // Check verification token expiry
            if (userEntity.EmailVerificationTokenExpiryTime == null ||
                userEntity.EmailVerificationTokenExpiryTime < DateTime.UtcNow)
                throw new BadRequestException("Pin Code expired");

            // Check pin code
            if (string.IsNullOrWhiteSpace(userEntity.EmailVerificationToken) == false &&
                userEntity.EmailVerificationToken.Equals(dto.PinCode) == false)
                throw new BadRequestException("Incorrect pin code");

            // All checks complete, Verify email address
            userEntity.EmailConfirmed = true;
            userEntity.EmailVerificationToken = null;
            userEntity.EmailVerificationTokenExpiryTime = null;
            await _userManager.UpdateAsync(userEntity);
        }
    }
}
