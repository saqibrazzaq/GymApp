using api.Data;
using api.Dtos.Invoice;
using api.Entities;
using api.Repository.Interfaces;
using api.Utility.Paging;

namespace api.Repository.Implementations
{
    public class InvoiceStatusRepository : RepositoryBase<InvoiceStatus>, IInvoiceStatusRepository
    {
        public InvoiceStatusRepository(AppDbContext context) : base(context)
        {
        }

        public PagedList<InvoiceStatus> Search(InvoiceStatusSearchReq dto, bool trackChanges)
        {
            var entities = FindAll(trackChanges)
                .Search(dto)
                .Sort(dto.OrderBy)
                .Skip((dto.PageNumber - 1) * dto.PageSize)
                .Take(dto.PageSize)
                .ToList();
            var count = FindAll(trackChanges: false)
                .Search(dto)
                .Count();
            return new PagedList<InvoiceStatus>(entities, count,
                dto.PageNumber, dto.PageSize);
        }
    }
}
