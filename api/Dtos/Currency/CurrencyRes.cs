using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Currency
{
    public class CurrencyRes
    {
        public int CurrencyId { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
    }
}
