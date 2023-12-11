using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Entities
{
    [Table("Plan")]
    public class Plan
    {
        [Key]
        public int PlanId { get; set; }
        [Required]
        public int? AccountId { get; set; }
        [ForeignKey(nameof(AccountId))]
        public Account? Account { get; set; }
        [Required, MinLength(3)]
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? PlanCategoryId { get; set; }
        [ForeignKey(nameof(PlanCategoryId))]
        public PlanCategory? PlanCategory { get; set; }
        public int? PlanTypeId { get; set; }
        [ForeignKey(nameof(PlanTypeId))]
        public PlanType? PlanType { get; set; }
        public int Duration { get; set; }
        public int? TimeUnitId { get; set; }
        [ForeignKey(nameof(TimeUnitId))]
        public TimeUnit? TimeUnit { get; set; }
        public int SetupFee { get; set; } = 0;
        public int Price { get; set; } = 0;
    }
}
