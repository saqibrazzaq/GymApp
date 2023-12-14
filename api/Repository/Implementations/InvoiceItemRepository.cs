using api.Data;
using api.Dtos.Invoice;
using api.Entities;
using api.Repository.Interfaces;
using api.Utility.Paging;

namespace api.Repository.Implementations
{
    public class InvoiceItemRepository : RepositoryBase<InvoiceItem>, IInvoiceItemRepository
    {
        public InvoiceItemRepository(AppDbContext context) : base(context)
        {
        }

        public PagedList<InvoiceItem> Search(InvoiceItemSearchReq dto, bool trackChanges)
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
            return new PagedList<InvoiceItem>(entities, count,
                dto.PageNumber, dto.PageSize);
        }
    }
}
