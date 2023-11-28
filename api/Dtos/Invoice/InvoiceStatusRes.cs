using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Invoice
{
    public class InvoiceStatusRes
    {
        public int InvoiceStatusId { get; set; }
        public string? Name { get; set; }
    }
}
