using api.Dtos.Account;
using api.Dtos.Payment;
using api.Repository.Interfaces;
using api.Services.Interfaces;
using api.Utility.Paging;
using AutoMapper;

namespace api.Services.Implementations
{
    public class PaymentMethodService : IPaymentMethodService
    {
        private readonly IRepositoryManager _rep;
        private readonly IMapper _mapper;
        public PaymentMethodService(IRepositoryManager rep, 
            IMapper mapper)
        {
            _rep = rep;
            _mapper = mapper;
        }

        public ApiOkPagedResponse<IEnumerable<PaymentMethodRes>, MetaData> Search(PaymentMethodSearchReq dto)
        {
            var pagedEntities = _rep.PaymentMethodRepository.
                Search(dto, false);
            var dtos = _mapper.Map<IEnumerable<PaymentMethodRes>>(pagedEntities);
            return new ApiOkPagedResponse<IEnumerable<PaymentMethodRes>, MetaData>(dtos,
                pagedEntities.MetaData);
        }
    }
}
