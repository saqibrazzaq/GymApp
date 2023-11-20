using api.Dtos.Plan;
using api.Dtos.PlanCategory;
using api.Entities;
using api.Repository.Interfaces;
using api.Services.Interfaces;
using api.Utility.Paging;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace api.Services.Implementations
{
    public class PlanService : IPlanService
    {
        private readonly IRepositoryManager _rep;
        private readonly IMapper _mapper;
        private readonly IMyProfileService _myProfileService;
        public PlanService(IRepositoryManager rep,
            IMapper mapper,
            IMyProfileService myProfileService)
        {
            _rep = rep;
            _mapper = mapper;
            _myProfileService = myProfileService;
        }

        public async Task<PlanRes> Create(PlanEditReq dto)
        {
            var user = await _myProfileService.GetLoggedInUser();

            var entity = _mapper.Map<Plan>(dto);
            entity.AccountId = user.AccountId;

            _rep.PlanRepository.Create(entity);
            _rep.Save();

            return _mapper.Map<PlanRes>(entity);
        }

        public async Task Delete(int planId)
        {
            var entity = await FindPlanIfExists(planId, true);
            _rep.PlanRepository.Delete(entity);
            _rep.Save();
        }

        private async Task<Plan> FindPlanIfExists(int planId, bool trackChanges)
        {
            var user = await _myProfileService.GetLoggedInUser();
            var entity = _rep.PlanRepository.FindByCondition(
                x => x.PlanId == planId &&
                    x.AccountId == user.AccountId,
                trackChanges,
                include: i => i
                .Include(x => x.PlanCategory)
                .Include(x => x.PlanType)
                )
                .FirstOrDefault();
            if (entity == null) { throw new Exception("No plan found with id " + planId); }

            return entity;
        }

        public async Task<PlanRes> Get(int planId)
        {
            var entity = await FindPlanIfExists(planId, false);
            return _mapper.Map<PlanRes>(entity);
        }

        public async Task<ApiOkPagedResponse<IEnumerable<PlanRes>, MetaData>> Search(
            PlanSearchReq dto)
        {
            var user = await _myProfileService.GetLoggedInUser();
            dto.AccountId = user.AccountId;

            var pagedEntities = _rep.PlanRepository.
                Search(dto, false);
            var dtos = _mapper.Map<IEnumerable<PlanRes>>(pagedEntities);
            return new ApiOkPagedResponse<IEnumerable<PlanRes>, MetaData>(dtos,
                pagedEntities.MetaData);
        }

        public async Task<PlanRes> Update(int planId, PlanEditReq dto)
        {
            var entity = await FindPlanIfExists(planId, true);
            _mapper.Map(dto, entity);
            _rep.Save();
            return _mapper.Map<PlanRes>(entity);
        }
    }
}
