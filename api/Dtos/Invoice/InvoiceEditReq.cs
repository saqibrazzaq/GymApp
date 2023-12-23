using api.Dtos.Address;
using api.Dtos.Country;
using api.Entities;

namespace api.Dtos.Invoice
{
    public class InvoiceEditReq
    {
        public string? UserId { get; set; }
        public string? Email { get; set; }
        public int? ShippingAddressId { get; set; }
        public string? FullName { get; set; }
        public string? Phone { get; set; } = "";
        public string? Address1 { get; set; } = "";
        public string? Address2 { get; set; } = "";
        public string? City { get; set; } = "";
        public int? StateId { get; set; }
        public DateTime IssueDate { get; set; } = DateTime.UtcNow;
        public int? StatusId { get; set; } = (int)InvoiceStatusNames.Draft;
    }
}
