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
    }
}
