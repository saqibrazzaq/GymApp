using api.Dtos.Country;
using api.Dtos.Currency;
using api.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Dtos.Account
{
    public class AccountRes
    {
        public int AccountId { get; set; }

        public int? AccountTypeId { get; set; }
        public AccountType? AccountType { get; set; }

        public string LogoUrl { get; set; } = "";
        public string? LogoCloudinaryId { get; set; }
        public string? CompanyName { get; set; }
        public string? CompanyWebsite { get; set; }
        public string? CompanyEmail { get; set; }
        public string? CompanyPhone { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? City { get; set; }
        public string? StateId { get; set; }
        public StateRes? State { get; set; }
        public string? BusinessLicense { get; set; }
        public string? FacebookPage { get; set; }
        public string? FacebookGroup { get; set; }
        public string? TwitterHandle { get; set; }
        public string? InstagramUsername { get; set; }
        public string? CurrencyId { get; set; }
        public CurrencyRes? Currency { get; set; }
    }
}
