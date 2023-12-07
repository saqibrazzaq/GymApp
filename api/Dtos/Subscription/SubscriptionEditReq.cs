using api.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Subscription
{
    public class SubscriptionEditReq
    {
        [Required]
        public int? PlanId { get; set; }
        public string? UserId { get; set; }
        public DateTime ActiveFrom { get; set; } = DateTime.MinValue;
        public DateTime ActiveTo { get; set; } = DateTime.MinValue;
    }
}
