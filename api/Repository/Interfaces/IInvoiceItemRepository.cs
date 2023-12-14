using api.Dtos.Invoice;
using api.Dtos.Plan;
using api.Entities;
using api.Utility.Paging;

namespace api.Repository.Interfaces
{
    public interface IInvoiceItemRepository : IRepositoryBase<InvoiceItem>
    {
        PagedList<InvoiceItem> Search(
            InvoiceItemSearchReq dto, bool trackChanges);
    }
}
