using api.Dtos.Account;
using api.Entities;
using api.Repository.Interfaces;
using api.Services.Interfaces;
using api.Storage;
using api.Utility;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace api.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly ICloudinaryService _cloudinaryService;
        private readonly IWebHostEnvironment _environment;
        private readonly IUserService _userService;
        private readonly IRepositoryManager _rep;
        private readonly IMapper _mapper;
        public AccountService(ICloudinaryService cloudinaryService,
            IWebHostEnvironment environment,
            IUserService userService,
            IRepositoryManager rep,
            IMapper mapper)
        {
            _cloudinaryService = cloudinaryService;
            _environment = environment;
            _userService = userService;
            _rep = rep;
            _mapper = mapper;
        }

        public async Task UpdateLogo(IFormFile file)
        {
            var uploadResult = _cloudinaryService.UploadProfilePictureThumbnail(file, TempFolderPath);
            await updateLogoInRepository(uploadResult);
        }

        private async Task updateLogoInRepository(CloudinaryUploadResultRes uploadResult)
        {
            var accountEntity = await FindMyAccountIfExists(true);
            //DeleteExistingProfilePictureFromCloudinary(userEntity.ProfilePictureCloudinaryId);
            _cloudinaryService.DeleteImage(accountEntity.LogoCloudinaryId);
            accountEntity.LogoUrl = uploadResult.SecureUrl;
            accountEntity.LogoCloudinaryId = uploadResult.PublicId;
            _rep.Save();
        }

        private async Task<Account> FindMyAccountIfExists(bool trackChanges)
        {
            var userEntity = await _userService.GetLoggedInUser();
            var entity = _rep.AccountRepository.FindByCondition(
                x => x.AccountId == userEntity.AccountId,
                trackChanges,
                include: i => i
                    .Include(x => x.State.Country)
                    .Include(x => x.Currency)
                    )
                .FirstOrDefault();
            if (entity == null) throw new Exception("Account not found");

            return entity;
        }

        public async Task<AccountRes> GetMyAccount()
        {
            var entity = await FindMyAccountIfExists(false);
            return _mapper.Map<AccountRes>(entity);
        }

        public async Task<AccountRes> Update(AccountEditReq dto)
        {
            var entity = await FindMyAccountIfExists(true);
            _mapper.Map(dto, entity);
            _rep.Save();
            return _mapper.Map<AccountRes>(entity);

        }

        public string TempFolderPath
        {
            get
            {
                return Path.Combine(_environment.WebRootPath, Constants.TempFolderName);
            }
        }

        
    }
}
