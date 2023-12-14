using api.Dtos.Invoice;
using api.Dtos.Plan;
using api.Entities;
using api.Utility.Paging;

namespace api.Repository.Interfaces
{
    public interface IInvoiceRepository : IRepositoryBase<Invoice>
    {
        PagedList<Invoice> Search(
            InvoiceSearchReq dto, bool trackChanges);
    }
}
