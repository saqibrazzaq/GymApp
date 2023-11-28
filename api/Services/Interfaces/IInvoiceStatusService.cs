using api.Dtos.Account;
using api.Dtos.Invoice;
using api.Utility.Paging;

namespace api.Services.Interfaces
{
    public interface IInvoiceStatusService
    {
        ApiOkPagedResponse<IEnumerable<InvoiceStatusRes>, MetaData> Search(InvoiceStatusSearchReq dto);
    }
}
