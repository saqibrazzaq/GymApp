using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Entities
{
    [Table("Discount")]
    public class Discount
    {
        [Key]
        public int DiscountId { get; set; }
        [Required]
        public string? Code { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public int? DiscountTypeId { get; set; }
        [ForeignKey(nameof(DiscountTypeId))]
        public DiscountType? DiscountType { get; set; }
        [Required]
        public int Value { get; set; }
    }
}
