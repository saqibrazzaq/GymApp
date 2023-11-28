using api.Dtos.Account;
using api.Dtos.Invoice;
using api.Entities;
using api.Utility.Paging;

namespace api.Repository.Interfaces
{
    public interface IInvoiceStatusRepository : IRepositoryBase<InvoiceStatus>
    {
        PagedList<InvoiceStatus> Search(InvoiceStatusSearchReq dto, bool trackChanges);
    }
}
