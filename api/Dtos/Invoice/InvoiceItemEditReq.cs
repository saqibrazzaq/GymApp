using api.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Invoice
{
    public class InvoiceItemEditReq
    {
        public int InvoiceId { get; set; }
        public int Qty { get; set; } = 1;
        public int Price { get; set; }
        public int TotalPrice { get; set; }
    }
}
