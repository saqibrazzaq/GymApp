using api.Dtos.Plan;
using api.Dtos.Subscription;
using api.Entities;
using api.Repository.Interfaces;
using api.Services.Interfaces;
using api.Utility.Paging;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace api.Services.Implementations
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly IRepositoryManager _rep;
        private readonly IMapper _mapper;
        private readonly IMyProfileService _myProfileService;
        private readonly UserManager<AppIdentityUser> _userManager;
        public SubscriptionService(IRepositoryManager rep,
            IMapper mapper,
            IMyProfileService myProfileService,
            UserManager<AppIdentityUser> userManager)
        {
            _rep = rep;
            _mapper = mapper;
            _myProfileService = myProfileService;
            _userManager = userManager;
        }

        public async Task<SubscriptionRes> Create(SubscriptionEditReq dto)
        {
            var currentUser = await _myProfileService.GetLoggedInUser();
            var user = await _userManager.FindByIdAsync(dto.UserId);
            if (user == null) { throw new Exception("Invalid user "); }
            if (user.AccountId != currentUser.AccountId)
                throw new Exception("User does not belong to this account " + user.FullName);

            var entity = _mapper.Map<Subscription>(dto);
            entity.ActiveTo = CalculateExpiryDate(entity);
            entity.Status = IsActive(entity.ActiveTo);
            _rep.SubscriptionRepository.Create(entity);
            _rep.Save();

            return _mapper.Map<SubscriptionRes>(entity);
        }

        private DateTime CalculateExpiryDate(Subscription sub)
        {
            var plan = _rep.PlanRepository.FindByCondition(
                x => x.PlanId == sub.PlanId,
                false)
                .FirstOrDefault();
            if (plan == null || plan.TimeUnitId == null) return DateTime.UtcNow;

            int daysToAdd = plan.Duration;
            TimeUnitNames timeUnit = (TimeUnitNames)plan.TimeUnitId;

            switch(timeUnit)
            {
                case TimeUnitNames.Day:
                    daysToAdd *= 1; break;
                case TimeUnitNames.Week:
                    daysToAdd *= 7; break;
                case TimeUnitNames.Month:
                    daysToAdd *= 30; break;
                case TimeUnitNames.Year:
                    daysToAdd *= 365; break;
                default:
                    daysToAdd = 0; break;
            }

            return sub.ActiveFrom.AddDays(daysToAdd);
        }

        public async Task Delete(int subscriptionId)
        {
            var entity = await FindSubscriptionIfExists(subscriptionId, true);
            _rep.SubscriptionRepository.Delete(entity);
            _rep.Save();
        }

        private async Task<Subscription> FindSubscriptionIfExists(int subscriptionId, bool trackChanges)
        {
            var user = await _myProfileService.GetLoggedInUser();
            var entity = _rep.SubscriptionRepository.FindByCondition(
                x => x.User.AccountId == user.AccountId &&
                x.SubscriptionId == subscriptionId,
                trackChanges,
                include: i => i
                    .Include(x => x.User)
                    .Include(x => x.Plan))
                .FirstOrDefault();

            if (entity == null) { throw new Exception("No subscription found with id " + subscriptionId); }

            return entity;
        }

        public async Task<SubscriptionRes> Get(int subscriptionId)
        {
            var entity = await FindSubscriptionIfExists(subscriptionId, false);
            return _mapper.Map<SubscriptionRes>(entity);
        }

        public async Task<ApiOkPagedResponse<IEnumerable<SubscriptionRes>, MetaData>> Search(
            SubscriptionSearchReq dto)
        {
            var user = await _myProfileService.GetLoggedInUser();
            dto.AccountId = user.AccountId;

            var pagedEntities = _rep.SubscriptionRepository.
                Search(dto, false);
            var dtos = _mapper.Map<IEnumerable<SubscriptionRes>>(pagedEntities);
            return new ApiOkPagedResponse<IEnumerable<SubscriptionRes>, MetaData>(dtos,
                pagedEntities.MetaData);
        }

        public async Task<SubscriptionRes> Update(int subscriptionId, SubscriptionEditReq dto)
        {
            var entity = await FindSubscriptionIfExists(subscriptionId, true);
            _mapper.Map(dto, entity);
            entity.ActiveTo = CalculateExpiryDate(entity);
            entity.Status = IsActive(entity.ActiveTo);
            _rep.Save();
            return _mapper.Map<SubscriptionRes>(entity);
        }

        private bool IsActive(DateTime activeTo)
        {
            if (DateTime.Compare(activeTo, DateTime.UtcNow) > 0)
                return true;
            else
                return false;
        }
    }
}
