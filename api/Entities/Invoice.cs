using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Entities
{
    [Table("Invoice")]
    public class Invoice
    {
        [Key]
        public int InvoiceId { get; set; }
        [Required]
        public int? AccountId { get; set; }
        [ForeignKey(nameof(AccountId))]
        public Account? Account { get; set; }
        [Required]
        public string? UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public AppIdentityUser? User { get; set; }
        public string? Email { get; set; }
        public int? ShippingAddressId { get; set; }
        [ForeignKey(nameof(ShippingAddressId))]
        public Address? ShippingAddress { get; set; }
        [Required]
        public string? FullName { get; set; }
        public string? Phone { get; set; } = "";
        public string? Address1 { get; set; } = "";
        public string? Address2 { get; set; } = "";
        public string? City { get; set; } = "";
        public int? StateId { get; set; }
        [ForeignKey(nameof(StateId))]
        public State? State { get; set; }
        public DateTime IssueDate { get; set; } = DateTime.UtcNow;
        [Required]
        public int? StatusId { get; set; } = (int)InvoiceStatusNames.Draft;
        [ForeignKey(nameof(StatusId))]
        public InvoiceStatus? Status { get; set; }
        public int AmountPayable { get; set; }
        public int AmountDue { get; set; }

        // Child tables
        public IList<InvoiceItem>? InvoiceItems { get; set; }
    }
}
