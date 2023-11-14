using api.Dtos.Plan;
using api.Dtos.User;
using api.Repository.Interfaces;
using api.Services.Interfaces;
using api.Utility.Paging;
using AutoMapper;

namespace api.Services.Implementations
{
    public class UserTypeService : IUserTypeService
    {
        private readonly IRepositoryManager _rep;
        private readonly IMapper _mapper;
        public UserTypeService(IRepositoryManager rep, 
            IMapper mapper)
        {
            _rep = rep;
            _mapper = mapper;
        }

        public ApiOkPagedResponse<IEnumerable<UserTypeRes>, MetaData> Search(UserTypeSearchReq dto)
        {
            var pagedEntities = _rep.UserTypeRepository.
                Search(dto, false);
            var dtos = _mapper.Map<IEnumerable<UserTypeRes>>(pagedEntities);
            return new ApiOkPagedResponse<IEnumerable<UserTypeRes>, MetaData>(dtos,
                pagedEntities.MetaData);
        }
    }
}
