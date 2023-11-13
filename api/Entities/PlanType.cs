using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Entities
{
    [Table("PlanType")]
    public class PlanType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PlanTypeId { get; set; }
        [Required]
        public string? Name { get; set; }

    }

    public enum PlanTypeNames
    {
        Recurring,
        NonRecurring,
        Punchcard
    }
}
