using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Entities
{
    [Table("Subscription")]
    public class Subscription
    {
        [Key]
        public int SubscriptionId { get; set; }
        [Required]
        public int? PlanId { get; set; }
        [ForeignKey(nameof(PlanId))]
        public Plan? Plan { get; set; }
        [Required]
        public string? UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public AppIdentityUser? User { get; set; }
        public DateTime ActiveFrom { get; set; } = DateTime.MinValue;
        public DateTime ActiveTo { get; set; } = DateTime.MinValue;
    }
}
