using api.Dtos.Account;
using api.Dtos.Invoice;
using api.Repository.Interfaces;
using api.Services.Interfaces;
using api.Utility.Paging;
using AutoMapper;

namespace api.Services.Implementations
{
    public class DiscountTypeService : IDiscountTypeService
    {
        private readonly IRepositoryManager _rep;
        private readonly IMapper _mapper;
        public DiscountTypeService(IRepositoryManager rep, 
            IMapper mapper)
        {
            _rep = rep;
            _mapper = mapper;
        }

        public ApiOkPagedResponse<IEnumerable<DiscountTypeRes>, MetaData> Search(
            DiscountTypeSearchReq dto)
        {
            var pagedEntities = _rep.DiscountTypeRepository.
                Search(dto, false);
            var dtos = _mapper.Map<IEnumerable<DiscountTypeRes>>(pagedEntities);
            return new ApiOkPagedResponse<IEnumerable<DiscountTypeRes>, MetaData>(dtos,
                pagedEntities.MetaData);
        }
    }
}
