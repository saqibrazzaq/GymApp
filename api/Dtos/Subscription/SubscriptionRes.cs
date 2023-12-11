using api.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using api.Dtos.Plan;
using api.Dtos.User;

namespace api.Dtos.Subscription
{
    public class SubscriptionRes
    {
        public int SubscriptionId { get; set; }
        public int? PlanId { get; set; }
        public PlanRes? Plan { get; set; }
        public string? UserId { get; set; }
        public UserRes? User { get; set; }
        public DateTime ActiveFrom { get; set; } = DateTime.MinValue;
        public DateTime ActiveTo { get; set; } = DateTime.MinValue;
        public bool Status { get; set; }
    }
}
