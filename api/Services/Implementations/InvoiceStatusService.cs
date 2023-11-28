using api.Dtos.Account;
using api.Dtos.Invoice;
using api.Repository.Interfaces;
using api.Services.Interfaces;
using api.Utility.Paging;
using AutoMapper;

namespace api.Services.Implementations
{
    public class InvoiceStatusService : IInvoiceStatusService
    {
        private readonly IRepositoryManager _rep;
        private readonly IMapper _mapper;
        public InvoiceStatusService(IRepositoryManager rep, 
            IMapper mapper)
        {
            _rep = rep;
            _mapper = mapper;
        }

        public ApiOkPagedResponse<IEnumerable<InvoiceStatusRes>, MetaData> Search(InvoiceStatusSearchReq dto)
        {
            var pagedEntities = _rep.InvoiceStatusRepository.
                Search(dto, false);
            var dtos = _mapper.Map<IEnumerable<InvoiceStatusRes>>(pagedEntities);
            return new ApiOkPagedResponse<IEnumerable<InvoiceStatusRes>, MetaData>(dtos,
                pagedEntities.MetaData);
        }
    }
}
