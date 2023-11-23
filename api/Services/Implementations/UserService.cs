using api.Common;
using api.Dtos.User;
using api.Entities;
using api.Mailer;
using api.Models.Exceptions;
using api.Repository.Interfaces;
using api.Services.Interfaces;
using api.Storage;
using api.Utility;
using api.Utility.Paging;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace api.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppIdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailSender _emailSender;
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IWebHostEnvironment _environment;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly IVerificationEmailSenderService _verificationEmailSenerService;

        public UserService(UserManager<AppIdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,
            IEmailSender emailSender,
            IRepositoryManager repository,
            IMapper mapper,
            IHttpContextAccessor contextAccessor,
            IWebHostEnvironment environment,
            ICloudinaryService cloudinaryService,
            IVerificationEmailSenderService verificationEmailSenerService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _emailSender = emailSender;
            _repository = repository;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
            _environment = environment;
            _cloudinaryService = cloudinaryService;
            _verificationEmailSenerService = verificationEmailSenerService;
        }

        public async Task Delete(DeleteUserReq dto)
        {
            var userEntity = await _userManager.FindByNameAsync(dto.Username);
            var currentUserEntity = await _userManager.FindByNameAsync(UserName);
            ValidateUserSameAccount(currentUserEntity, userEntity);
            
            var roles = await _userManager.GetRolesAsync(userEntity);
            if (roles.Contains(Constants.OwnerRole))
                throw new BadRequestException("Owner user cannot be deleted");
            if (roles.Contains(Constants.SuperAdminRole))
                throw new BadRequestException("Super Admin user cannot be deleted");

            
            var resultUser = await _userManager.DeleteAsync(userEntity);
            if (resultUser.Succeeded == false)
            {
                throw new BadRequestException(AuthUtil.GetFirstErrorFromIdentityResult(
                    resultUser, nameof(Delete)));
            }
        }

        public async Task<UserRes> FindByUsername(string username)
        {
            // Find the user
            var userEntity = await _userManager.FindByNameAsync(username);
            // Find current user
            var currentUserEntity = await _userManager.FindByNameAsync(UserName);
            ValidateUserSameAccount(currentUserEntity, userEntity);

            var userDto = _mapper.Map<UserRes>(userEntity);
            userDto.Roles = await _userManager.GetRolesAsync(userEntity);
            return userDto;
        }

        protected async Task CheckExistingEmail(string email)
        {
            // Email and username must not already exist
            var userEntity = await _userManager.FindByEmailAsync(email);
            if (userEntity != null)
                throw new BadRequestException($"Email {email} is already registered. Use Forgot password if you own this account.");
        }

        public async Task UpdateUser(string email, UserEditReq dto)
        {
            var currentUserEntity = await _userManager.FindByEmailAsync(UserName);
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null) throw new Exception("No user found with email " + email);
            if (currentUserEntity.AccountId != user.AccountId)
                throw new Exception("No user found in this account " + email);

            _mapper.Map(dto, user);
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded == false)
            {
                throw new Exception(result.Errors.FirstOrDefault().Description);
            }
        }

        protected void ValidateUserSameAccount(AppIdentityUser? currentUserEntity, AppIdentityUser? userEntity)
        {
            if (userEntity == null)
                throw new Exception("User not found");
            if (currentUserEntity == null)
                throw new Exception("User not found");
            if (userEntity.AccountId != currentUserEntity.AccountId)
                throw new Exception("User does not belong to this account.");
        }

        public async Task UpdateProfilePicture(string email, IFormFile file)
        {
            var uploadResult = _cloudinaryService.UploadProfilePictureThumbnail(file, TempFolderPath);
            await updateProfilePictureInRepository(email, uploadResult);
        }

        public async Task SetNewPassword(string email, SetNewPasswordReq dto)
        {
            var userEntity = await _userManager.FindByEmailAsync(email);
            if (userEntity == null) throw new Exception("User not found with email " + email);

            var currentUserEntity = await _userManager.FindByEmailAsync(UserName);
            if (currentUserEntity.AccountId != userEntity.AccountId)
                throw new Exception("User not found in this account: " + email);

            var hasPassword = await _userManager.HasPasswordAsync(userEntity);
            if (hasPassword)
            {
                var removeResult = await _userManager.RemovePasswordAsync(userEntity);
                if (removeResult.Succeeded == false)
                    throw new Exception(AuthUtil.GetFirstErrorFromIdentityResult(removeResult, nameof(SetNewPassword)));
            }

            var newResult = await _userManager.AddPasswordAsync(userEntity, dto.NewPassword);
            if (newResult.Succeeded == false)
                throw new Exception(AuthUtil.GetFirstErrorFromIdentityResult(newResult, nameof(SetNewPassword)));
        }

        private async Task updateProfilePictureInRepository(string email, CloudinaryUploadResultRes uploadResult)
        {
            var userEntity = await _userManager.FindByNameAsync(email);
            var currentUser = await _userManager.FindByNameAsync(UserName);
            
            if (userEntity == null) throw new Exception("Username not found " + email);
            if (userEntity.AccountId != currentUser.AccountId) 
                throw new Exception("Username not found in this account " + email);
            
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

        protected string? UserName
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
    }
}
