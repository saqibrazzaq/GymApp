using api.Dtos.Country;
using api.Dtos.Currency;
using api.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Dtos.Account
{
    public class AccountEditReq
    {
        public string? CompanyName { get; set; }
        public string? CompanyWebsite { get; set; }
        public string? CompanyEmail { get; set; }
        public string? CompanyPhone { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? City { get; set; }
        public int? StateId { get; set; }
        public string? BusinessLicense { get; set; }
        public string? FacebookPage { get; set; }
        public string? FacebookGroup { get; set; }
        public string? TwitterHandle { get; set; }
        public string? InstagramUsername { get; set; }
        public int? CurrencyId { get; set; }
    }
}
