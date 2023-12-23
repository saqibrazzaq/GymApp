using api.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using api.Dtos.Country;
using api.Dtos.User;
using api.Dtos.Address;

namespace api.Dtos.Invoice
{
    public class InvoiceRes
    {
        public int InvoiceId { get; set; }
        public int? AccountId { get; set; }
        public string? UserId { get; set; }
        public UserRes? User { get; set; }
        public string? Email { get; set; }
        public int? ShippingAddressId { get; set; }
        public AddressRes? ShippingAddress { get; set; }
        public string? FullName { get; set; }
        public string? Phone { get; set; } = "";
        public string? Address1 { get; set; } = "";
        public string? Address2 { get; set; } = "";
        public string? City { get; set; } = "";
        public int? StateId { get; set; }
        public StateRes State { get; set; }
        public DateTime IssueDate { get; set; } = DateTime.UtcNow;
        public int? StatusId { get; set; } = (int)InvoiceStatusNames.Draft;
        public InvoiceStatusRes Status { get; set; }
        public int AmountPayable { get; set; }
        public int AmountDue { get; set; }
        public IList<InvoiceItemRes>? InvoiceItems { get; set; }
    }
}
