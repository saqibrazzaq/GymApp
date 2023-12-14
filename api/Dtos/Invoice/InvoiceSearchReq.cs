using api.Utility.Paging;

namespace api.Dtos.Invoice
{
    public class InvoiceSearchReq : PagedReq
    {
        public int? AccountId { get; set; } = 0;
    }
}
