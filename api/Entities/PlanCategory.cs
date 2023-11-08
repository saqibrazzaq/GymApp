using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Entities
{
    [Table("PlanCategory")]
    public class PlanCategory
    {
        [Key]
        public int PlanCategoryId { get; set; }
        [Required]
        public int? AccountId { get; set; }
        [ForeignKey(nameof(AccountId))]
        public Account? Account { get; set; }

        [Required, MinLength(3)]
        public string? Name { get; set; }
    }
}
