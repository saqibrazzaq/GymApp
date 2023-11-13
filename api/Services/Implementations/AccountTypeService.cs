using api.Dtos.Account;
using api.Dtos.Plan;
using api.Repository.Interfaces;
using api.Services.Interfaces;
using api.Utility.Paging;
using AutoMapper;

namespace api.Services.Implementations
{
    public class AccountTypeService : IAccountTypeService
    {
        private readonly IRepositoryManager _rep;
        private readonly IMapper _mapper;
        public AccountTypeService(IRepositoryManager rep, 
            IMapper mapper)
        {
            _rep = rep;
            _mapper = mapper;
        }

        public ApiOkPagedResponse<IEnumerable<AccountTypeRes>, MetaData> Search(AccountTypeSearchReq dto)
        {
            var pagedEntities = _rep.AccountTypeRepository.
                Search(dto, false);
            var dtos = _mapper.Map<IEnumerable<AccountTypeRes>>(pagedEntities);
            return new ApiOkPagedResponse<IEnumerable<AccountTypeRes>, MetaData>(dtos,
                pagedEntities.MetaData);
        }
    }
}
