using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Entities
{
    [Table("InvoiceStatus")]
    public class InvoiceStatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int InvoiceStatusId { get; set; }
        [Required]
        public string? Name { get; set; }
    }

    public enum InvoiceStatusNames
    {
        Draft = 1,
        Sent = 2,
        Paid = 3,
        NoCharge = 4
    }
}
