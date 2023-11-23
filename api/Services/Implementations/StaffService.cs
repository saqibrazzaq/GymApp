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
    public class StaffService : UserService, IStaffService
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

        public StaffService(UserManager<AppIdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,
            IEmailSender emailSender,
            IRepositoryManager repository,
            IMapper mapper,
            IHttpContextAccessor contextAccessor,
            IWebHostEnvironment environment,
            ICloudinaryService cloudinaryService,
            IVerificationEmailSenderService verificationEmailSenerService)
            : base(userManager, 
                  roleManager, 
                  configuration, 
                  emailSender, 
                  repository, 
                  mapper, 
                  contextAccessor, 
                  environment, 
                  cloudinaryService, 
                  verificationEmailSenerService)
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

        public async Task<ApiOkPagedResponse<IList<UserRes>, MetaData>> SearchUsers(
            SearchUsersReq dto, bool trackChanges)
        {
            var userEntity = await _userManager.FindByNameAsync(UserName ?? "");
            dto.AccountId = userEntity?.AccountId ?? 0;

            var usersWithMetadata = _repository.UserRepository.SearchStaff(
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

        public async Task AddRoleToStaff(AddRoleReq dto)
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

        private bool canAddRole(string? roleName)
        {
            var roles = Constants.AssignableRoles.Split(',');
            var role = roles.Where(x => x == roleName).FirstOrDefault();
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

        public async Task RemoveRoleFromStaff(RemoveRoleReq dto)
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
    }
}
