using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Entities
{
    public class AppIdentityUser : IdentityUser
    {
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        public string? EmailVerificationToken { get; set; }
        public DateTime? EmailVerificationTokenExpiryTime { get; set; }
        public string ProfilePictureUrl { get; set; } = "";
        public string? ProfilePictureCloudinaryId { get; set; }
        public string FullName { get; set; } = "";

        // Foreign keys
        public int? AccountId { get; set; }
        [ForeignKey(nameof(AccountId))]
        public Account? Account { get; set; }
        public int? UserTypeId { get; set; }
        [ForeignKey(nameof(UserTypeId))]
        public UserType? UserType { get; set; }
        public int? LeadStatusId { get; set; }
        [ForeignKey(nameof(LeadStatusId))]
        public LeadStatus? LeadStatus { get; set; }
        public int? GenderId { get; set; }
        [ForeignKey(nameof(GenderId))]
        public Gender? Gender { get; set; }
    }
}
