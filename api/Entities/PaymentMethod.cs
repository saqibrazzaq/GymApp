using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Entities
{
    [Table("PaymentMethod")]
    public class PaymentMethod
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PaymentMethodId { get; set; }
        [Required]
        public string? Name { get; set; }
    }

    public enum PaymentMethodNames
    {
        Cash = 1,
        CreditCard = 2,
        BankTransfer = 3
    }
}
