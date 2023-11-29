using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Payment
{
    public class PaymentMethodRes
    {
        public int PaymentMethodId { get; set; }
        public string? Name { get; set; }
    }
}
