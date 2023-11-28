using api.Dtos.Plan;
using api.Dtos.User;
using api.Repository.Interfaces;
using api.Services.Interfaces;
using api.Utility.Paging;
using AutoMapper;

namespace api.Services.Implementations
{
    public class GenderService : IGenderService
    {
        private readonly IRepositoryManager _rep;
        private readonly IMapper _mapper;
        public GenderService(IRepositoryManager rep, 
            IMapper mapper)
        {
            _rep = rep;
            _mapper = mapper;
        }

        public ApiOkPagedResponse<IEnumerable<GenderRes>, MetaData> Search(GenderSearchReq dto)
        {
            var pagedEntities = _rep.GenderRepository.
                Search(dto, false);
            var dtos = _mapper.Map<IEnumerable<GenderRes>>(pagedEntities);
            return new ApiOkPagedResponse<IEnumerable<GenderRes>, MetaData>(dtos,
                pagedEntities.MetaData);
        }
    }
}
