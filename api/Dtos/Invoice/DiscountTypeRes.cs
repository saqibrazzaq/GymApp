using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Invoice
{
    public class DiscountTypeRes
    {
        public int DiscountTypeId { get; set; }
        public string? Name { get; set; }
    }
}
