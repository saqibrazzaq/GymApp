using api.Utility.Paging;

namespace api.Dtos.Invoice
{
    public class InvoiceItemSearchReq : PagedReq
    {
        public int? AccountId { get; set; } = 0;
        public int InvoiceId { get; set; }
    }
}
