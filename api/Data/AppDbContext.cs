using api.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }

        // Tables in Database context
        public new DbSet<AppIdentityUser>? Users { get; set; }
        public DbSet<Account>? Accounts { get; set; }
        public DbSet<AccountType>? AccountTypes { get; set; }
        public DbSet<Country>? Countries { get; set; }
        public DbSet<State>? States { get; set; }
        public DbSet<Address>? Addresses { get; set; }
        public DbSet<UserAddress>? UserAddresses { get; set; }
        public DbSet<Currency>? Currencies { get; set; }
        public DbSet<PlanCategory>? PlanCategories { get; set; }
        public DbSet<PlanType>? PlanTypes { get; set; }
        public DbSet<Plan>? Plans { get; set; }
        public DbSet<UserType>? UserTypes { get; set; }
        public DbSet<LeadStatus>? LeadStatuses { get; set; }
        public DbSet<Gender>? Genders { get; set; }
        public DbSet<DiscountType>? DiscountTypes { get; set; }
        public DbSet<InvoiceStatus>? InvoiceStatuses { get; set; }
        public DbSet<PaymentMethod>? PaymentMethods { get; set; }
        public DbSet<Discount>? Discounts { get; set; }
        public DbSet<Invoice>? Invoices { get; set; }
        public DbSet<InvoiceDiscount>? InvoiceDiscounts { get; set; }
        public DbSet<Payment>? Payments { get; set; }
        public DbSet<Subscription>? Subscriptions { get; set; }

    }
}
