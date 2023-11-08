using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Entities
{
    [Table("Account")]
    public class Account
    {
        [Key]
        public int AccountId { get; set; }

        // Foreign keys
        public int? AccountTypeId { get; set; }
        [ForeignKey(nameof(AccountTypeId))]
        public AccountType? AccountType { get; set; }

        public string LogoUrl { get; set; } = "";
        public string? LogoCloudinaryId { get; set; } = "";
        public string? CompanyName { get; set; } = "";
        public string? CompanyWebsite { get; set; } = "";
        public string? CompanyEmail { get; set; } = "";
        public string? CompanyPhone { get; set; } = "";
        public string? Address1 { get; set; } = "";
        public string? Address2 { get; set; } = "";
        public string? City { get; set; } = "";
        public int? StateId { get; set; }
        [ForeignKey(nameof(StateId))]
        public State? State { get; set; }
        public string? BusinessLicense { get; set; } = "";
        public string? FacebookPage { get; set; } = "";
        public string? FacebookGroup { get; set; } = "";
        public string? TwitterHandle { get; set; } = "";
        public string? InstagramUsername { get; set; } = "";
        public int? CurrencyId { get; set; }
        [ForeignKey(nameof(CurrencyId))]
        public Currency? Currency { get; set; }
    }
}
