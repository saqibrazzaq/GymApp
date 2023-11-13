using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Entities
{
    [Table("AccountType")]
    public class AccountType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AccountTypeId { get; set; }
        [Required]
        public string? Name { get; set; }
    }

    public enum AccountTypeNames
    {
        Unlimited = 1,
        Free = 2,
        Basic = 3,
        Pro = 4
    }
}
