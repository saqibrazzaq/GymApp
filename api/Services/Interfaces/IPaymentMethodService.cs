using api.Dtos.Account;
using api.Dtos.Payment;
using api.Utility.Paging;

namespace api.Services.Interfaces
{
    public interface IPaymentMethodService
    {
        ApiOkPagedResponse<IEnumerable<PaymentMethodRes>, MetaData> Search(PaymentMethodSearchReq dto);
    }
}
