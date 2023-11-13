using api.Dtos.Country;
using api.Dtos.Plan;
using api.Repository.Interfaces;
using api.Services.Interfaces;
using api.Utility.Paging;
using AutoMapper;

namespace api.Services.Implementations
{
    public class PlanTypeService : IPlanTypeService
    {
        private readonly IRepositoryManager _rep;
        private readonly IMapper _mapper;
        public PlanTypeService(IRepositoryManager rep, 
            IMapper mapper)
        {
            _rep = rep;
            _mapper = mapper;
        }

        public ApiOkPagedResponse<IEnumerable<PlanTypeRes>, MetaData> Search(PlanTypeSearchReq dto)
        {
            var pagedEntities = _rep.PlanTypeRepository.
                Search(dto, false);
            var dtos = _mapper.Map<IEnumerable<PlanTypeRes>>(pagedEntities);
            return new ApiOkPagedResponse<IEnumerable<PlanTypeRes>, MetaData>(dtos,
                pagedEntities.MetaData);
        }
    }
}
