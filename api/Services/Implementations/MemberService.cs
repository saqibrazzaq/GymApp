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
    public class MemberService : UserService, IMemberService
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

        public MemberService(UserManager<AppIdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,
            IEmailSender emailSender,
            IRepositoryManager repository,
            IMapper mapper,
            IHttpContextAccessor contextAccessor,
            IWebHostEnvironment environment,
            ICloudinaryService cloudinaryService,
            IVerificationEmailSenderService verificationEmailSenerService)
            : base (userManager, 
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

        public async Task CreateMember(StaffCreateReq dto)
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
                UserTypeId = (int)UserTypeNames.Member,
            };
            var resultUser = await _userManager.CreateAsync(userEntity, dto.Password);

            if (resultUser.Succeeded == false)
                throw new BadRequestException(AuthUtil.GetFirstErrorFromIdentityResult(
                    resultUser, nameof(CreateMember)));

            // Assign default role
            var roleResult = await _userManager.AddToRoleAsync(userEntity, Constants.UserRole);
            if (roleResult.Succeeded == false)
                throw new BadRequestException(AuthUtil.GetFirstErrorFromIdentityResult(
                    roleResult, nameof(CreateMember)));

            await _verificationEmailSenerService.SendEmail(userEntity.Email);
        }

        public async Task<ApiOkPagedResponse<IList<UserRes>, MetaData>> SearchMembers(
            SearchUsersReq dto, bool trackChanges)
        {
            var userEntity = await _userManager.FindByNameAsync(UserName ?? "");
            dto.AccountId = userEntity?.AccountId ?? 0;

            var usersWithMetadata = _repository.UserRepository.SearchMembers(
                dto, trackChanges);
            var usersDto = _mapper.Map<IList<UserRes>>(usersWithMetadata);
            return new ApiOkPagedResponse<IList<UserRes>, MetaData>(
                usersDto, usersWithMetadata.MetaData);
        }
    }
}
