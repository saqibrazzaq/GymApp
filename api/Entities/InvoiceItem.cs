using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Entities
{
    [Table("InvoiceItem")]
    public class InvoiceItem
    {
        [Key]
        public int InvoiceItemId { get; set; }
        [Required]
        public int? InvoiceId { get; set; }
        [ForeignKey(nameof(InvoiceId))]
        public Invoice? Invoice { get; set; }
        public int ProductId { get; set; }
        [Required]
        public string? ProductName { get; set; }
        [Required]
        public int Qty { get; set; } = 1;
        [Required]
        public int Price { get; set; }
        public int TotalPrice { get; set; }
    }
}
