using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Invoice
{
    public class InvoiceItemRes
    {
        public int InvoiceItemId { get; set; }
        public int? InvoiceId { get; set; }
        public InvoiceRes? Invoice { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public int Qty { get; set; } = 1;
        public int Price { get; set; }
        public int TotalPrice { get; set; }
    }
}
