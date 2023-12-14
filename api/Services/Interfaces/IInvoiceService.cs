using api.Dtos.Invoice;
using api.Dtos.Plan;
using api.Utility.Paging;

namespace api.Services.Interfaces
{
    public interface IInvoiceService
    {
        Task<InvoiceRes> Create(InvoiceEditReq dto);
        Task<InvoiceRes> Update(int invoiceId, InvoiceEditReq dto);
        Task<InvoiceItemRes> CreateItem(InvoiceItemEditReq dto);
        Task<InvoiceItemRes> UpdateItem(int invoiceItemId, InvoiceItemEditReq dto);
        Task<InvoiceRes> Get(int invoiceId);
        Task<InvoiceItemRes> GetItem(int invoiceItemId);
        Task Delete(int invoiceId);
        Task DeleteItem(int invoiceItemId);
        Task<ApiOkPagedResponse<IEnumerable<InvoiceRes>, MetaData>> Search(
            InvoiceSearchReq dto);
        Task<ApiOkPagedResponse<IEnumerable<InvoiceItemRes>, MetaData>> SearchItems(
            InvoiceItemSearchReq dto);
    }
}
