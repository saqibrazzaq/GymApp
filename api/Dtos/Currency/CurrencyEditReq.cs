using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Currency
{
    public class CurrencyEditReq
    {
        [Required, MinLength(3), MaxLength(3)]
        public string? Code { get; set; }
        [Required, MinLength(3)]
        public string? Name { get; set; }
    }
}
