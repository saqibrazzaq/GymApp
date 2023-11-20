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

        private async Task CheckExistingEmail(string email)
        {
            // Email and username must not already exist
            var userEntity = await _userManager.FindByEmailAsync(email);
            if (userEntity != null)
                throw new BadRequestException($"Email {email} is already registered. Use Forgot password if you own this account.");
        }

        public async Task CreateStaff(StaffCreateReq dto)
        {
            await CheckExistingEmail(dto.Email);
            var currentUserEntity = await _userManager.FindByEmailAsync(UserName);

            // Create new user
            var userEntity = new AppIdentityUser
            {
                UserName = dto.Email,
                Email = dto.Email,
                AccountId = currentUserEntity.AccountId,
                FullName = dto.FullName,
                UserTypeId = (int)UserTypeNames.Staff,
            };
            var resultUser = await _userManager.CreateAsync(userEntity, dto.Password);

            if (resultUser.Succeeded == false)
                throw new BadRequestException(AuthUtil.GetFirstErrorFromIdentityResult(
                    resultUser, nameof(CreateStaff)));

            // Assign default role
            var roleResult = await _userManager.AddToRoleAsync(userEntity, Constants.ManagerRole);
            if (roleResult.Succeeded == false)
                throw new BadRequestException(AuthUtil.GetFirstErrorFromIdentityResult(
                    roleResult, nameof(CreateStaff)));

            await _verificationEmailSenerService.SendEmail(userEntity.Email);
        }

        public async Task UpdateStaff(string email, StaffEditReq dto)
        {
            var currentUserEntity = await _userManager.FindByEmailAsync(UserName);
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null) throw new Exception("No staff user found with email " + email);

            _mapper.Map(dto, user);
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded == false)
            {
                throw new Exception(result.Errors.FirstOrDefault().Description);
            }
        }

        

        public async Task<ApiOkPagedResponse<IList<UserRes>, MetaData>> SearchUsers(
            SearchUsersReq dto, bool trackChanges)
        {
            var userEntity = await _userManager.FindByNameAsync(UserName ?? "");
            dto.AccountId = userEntity?.AccountId ?? 0;

            var usersWithMetadata = _repository.UserRepository.SearchUsers(
                dto, trackChanges);
            var usersDto = _mapper.Map<IList<UserRes>>(usersWithMetadata);
            for (int i = 0; i < usersDto.Count; i++)
            {
                AppIdentityUser user = await _userManager.FindByEmailAsync(usersDto[i].Email);
                usersDto[i].Roles = await _userManager.GetRolesAsync(user);
            }
            return new ApiOkPagedResponse<IList<UserRes>, MetaData>(
                usersDto, usersWithMetadata.MetaData);
        }

        public async Task AddRoleToUser(AddRoleReq dto)
        {
            if (canAddRole(dto.RoleName) == false)
                throw new Exception("Cannot add Role " + dto.RoleName + " to user.");
            var currentUserEntity = await _userManager.FindByNameAsync(UserName);
            var userEntity = await _userManager.FindByNameAsync(dto.UserName);
            ValidateUserSameAccount(currentUserEntity, userEntity);

            var result = await _userManager.AddToRoleAsync(userEntity, dto.RoleName);
            if (result.Succeeded == false)
                throw new Exception(result.Errors.FirstOrDefault().Description);
        }

        private void ValidateUserSameAccount(AppIdentityUser? currentUserEntity, AppIdentityUser? userEntity)
        {
            if (userEntity == null)
                throw new Exception("User not found");
            if (currentUserEntity == null)
                throw new Exception("User not found");
            if (userEntity.AccountId != currentUserEntity.AccountId)
                throw new Exception("User does not belong to this account.");
        }

        private bool canAddRole(string? roleName)
        {
            var roles = Constants.AssignableRoles.Split(',');
            var role = roles.Where(x => x ==  roleName).FirstOrDefault();
            if (role != null)
                return true;
            else
                return false;
        }

        private bool canRemoveRole(string? roleName)
        {
            var roles = Constants.AssignableRoles.Split(',');
            var role = roles.Where(x => x == roleName).FirstOrDefault();
            if (role != null)
                return true;
            else
                return false;
        }

        public IList<RoleRes> GetAllRoles()
        {
            var roles = Constants.AssignableRoles.Split(',');
            var list = roles.Select(x => new RoleRes { RoleName = x }).ToList();
            return list;
        }

        public async Task RemoveRoleFromUser(RemoveRoleReq dto)
        {
            if (canRemoveRole(dto.RoleName) == false)
                throw new Exception("Cannot remove Role " + dto.RoleName + " from user.");
            var currentUserEntity = await _userManager.FindByNameAsync(UserName);
            var userEntity = await _userManager.FindByNameAsync(dto.UserName);
            ValidateUserSameAccount(currentUserEntity, userEntity);
            
            var result = await _userManager.RemoveFromRoleAsync(userEntity, dto.RoleName);
            if (result.Succeeded == false)
                throw new Exception(result.Errors.FirstOrDefault().Description);
        }

        public async Task UpdateProfilePicture(string email, IFormFile file)
        {
            var uploadResult = _cloudinaryService.UploadProfilePictureThumbnail(file, TempFolderPath);
            await updateProfilePictureInRepository(email, uploadResult);
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
    }
}
