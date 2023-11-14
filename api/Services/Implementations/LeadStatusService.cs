using api.Dtos.Plan;
using api.Dtos.User;
using api.Repository.Interfaces;
using api.Services.Interfaces;
using api.Utility.Paging;
using AutoMapper;

namespace api.Services.Implementations
{
    public class LeadStatusService : ILeadStatusService
    {
        private readonly IRepositoryManager _rep;
        private readonly IMapper _mapper;
        public LeadStatusService(IRepositoryManager rep, 
            IMapper mapper)
        {
            _rep = rep;
            _mapper = mapper;
        }

        public ApiOkPagedResponse<IEnumerable<LeadStatusRes>, MetaData> Search(LeadStatusSearchReq dto)
        {
            var pagedEntities = _rep.LeadStatusRepository.
                Search(dto, false);
            var dtos = _mapper.Map<IEnumerable<LeadStatusRes>>(pagedEntities);
            return new ApiOkPagedResponse<IEnumerable<LeadStatusRes>, MetaData>(dtos,
                pagedEntities.MetaData);
        }
    }
}
