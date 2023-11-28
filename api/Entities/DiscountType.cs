using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Entities
{
    [Table("DiscountType")]
    public class DiscountType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DiscountTypeId { get; set; }
        [Required]
        public string? Name { get; set; }
    }

    public enum DiscountTypeNames
    {
        FixedAmount = 1,
        Percentage = 2
    }
}
