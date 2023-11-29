using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Entities
{
    [Table("InvoiceDiscount")]
    public class InvoiceDiscount
    {
        [Key]
        public int InvoiceDiscountId { get; set; }
        [Required]
        public int? InvoiceId { get; set; }
        [ForeignKey(nameof(InvoiceId))]
        public Invoice? Invoice { get; set; }
        [Required]
        public int DiscountId { get; set; }
        [ForeignKey(nameof(DiscountId))]
        public Discount? Discount { get; set; }
    }
}
