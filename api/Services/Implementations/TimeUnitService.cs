using api.Dtos.Plan;
using api.Repository.Interfaces;
using api.Services.Interfaces;
using api.Utility.Paging;
using AutoMapper;

namespace api.Services.Implementations
{
    public class TimeUnitService : ITimeUnitService
    {
        private readonly IRepositoryManager _rep;
        private readonly IMapper _mapper;
        public TimeUnitService(IRepositoryManager rep, 
            IMapper mapper)
        {
            _rep = rep;
            _mapper = mapper;
        }

        public ApiOkPagedResponse<IEnumerable<TimeUnitRes>, MetaData> Search(TimeUnitSearchReq dto)
        {
            var pagedEntities = _rep.TimeUnitRepository.
                Search(dto, false);
            var dtos = _mapper.Map<IEnumerable<TimeUnitRes>>(pagedEntities);
            return new ApiOkPagedResponse<IEnumerable<TimeUnitRes>, MetaData>(dtos,
                pagedEntities.MetaData);
        }
    }
}
