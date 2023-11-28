﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Entities
{
    [Table("Invoice")]
    public class Invoice
    {
        [Key]
        public int InvoiceId { get; set; }
        [Required]
        public int? SubscriptionId { get; set; }
        [ForeignKey(nameof(SubscriptionId))]
        public Subscription? Subscription { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? FullName { get; set; }
        public string? Phone { get; set; } = "";
        public string? Address1 { get; set; } = "";
        public string? Address2 { get; set; } = "";
        public string? City { get; set; } = "";
        public int? StateId { get; set; }
        [ForeignKey(nameof(StateId))]
        public State? State { get; set; }
        public DateTime IssueDate { get; set; } = DateTime.UtcNow;
        [Required]
        public int? StatusId { get; set; }


    }
}
