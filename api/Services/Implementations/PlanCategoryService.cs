using api.Dtos.Country;
using api.Dtos.PlanCategory;
using api.Entities;
using api.Repository.Interfaces;
using api.Services.Interfaces;
using api.Utility.Paging;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace api.Services.Implementations
{
    public class PlanCategoryService : IPlanCategoryService
    {
        private readonly IRepositoryManager _rep;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        public PlanCategoryService(IRepositoryManager rep,
            IMapper mapper,
            IUserService userService)
        {
            _rep = rep;
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<PlanCategoryRes> Create(PlanCategoryEditReq dto)
        {
            var user = await _userService.GetLoggedInUser();

            var entity = _mapper.Map<PlanCategory>(dto);
            entity.AccountId = user.AccountId;

            _rep.PlanCategoryRepository.Create(entity);
            _rep.Save();
            return _mapper.Map<PlanCategoryRes>(entity);
        }

        public async Task Delete(int planCategoryId)
        {
            var entity = await FindPlanCategoryIfExists(planCategoryId, true);
            _rep.PlanCategoryRepository.Delete(entity);
            _rep.Save();
        }

        private async Task<PlanCategory> FindPlanCategoryIfExists(int planCategoryId, bool trackChanges)
        {
            var user = await _userService.GetLoggedInUser();
            var entity = _rep.PlanCategoryRepository.FindByCondition(
                x => x.PlanCategoryId == planCategoryId &&
                    x.AccountId == user.AccountId,
                trackChanges)
                .FirstOrDefault();
            if (entity == null) { throw new Exception("No Plan Category found with id " + planCategoryId); }

            return entity;
        }

        public async Task<PlanCategoryRes> Get(int planCategoryId)
        {
            var entity = await FindPlanCategoryIfExists(planCategoryId, false);
            return _mapper.Map<PlanCategoryRes>(entity);
        }

        public async Task<ApiOkPagedResponse<IEnumerable<PlanCategoryRes>, MetaData>> Search(
            PlanCategorySearchReq dto)
        {
            var user = await _userService.GetLoggedInUser();
            dto.AccountId = user.AccountId;

            var pagedEntities = _rep.PlanCategoryRepository.
                Search(dto, false);
            var dtos = _mapper.Map<IEnumerable<PlanCategoryRes>>(pagedEntities);
            return new ApiOkPagedResponse<IEnumerable<PlanCategoryRes>, MetaData>(dtos,
                pagedEntities.MetaData);
        }

        public async Task<PlanCategoryRes> Update(int planCategoryId, PlanCategoryEditReq dto)
        {
            var entity = await FindPlanCategoryIfExists(planCategoryId, false);
            _mapper.Map(dto, entity);
            _rep.Save();
            return _mapper.Map<PlanCategoryRes>(entity);
        }
    }
}
