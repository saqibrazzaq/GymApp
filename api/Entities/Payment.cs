using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Entities
{
    [Table("Payment")]
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }
        [Required]
        public int? AccountId { get; set; }
        [ForeignKey(nameof(AccountId))]
        public Account? Account { get; set; }
        [Required]
        public int? InvoiceId { get; set; }
        [ForeignKey(nameof(InvoiceId))]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public Invoice? Invoice { get; set; }
        [Required]
        public int? PaymentMethodId { get; set; }
        [ForeignKey(nameof(PaymentMethodId))]
        public PaymentMethod? PaymentMethod { get; set; }
        public int AmountPaid { get; set; }
    }
}
