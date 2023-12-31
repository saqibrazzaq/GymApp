﻿using api.Services.Interfaces;

namespace api.Services.Implementations
{
    public class DataSeedService : IDataSeedService
    {
        private readonly IAccountTypeDataSeedService _accountTypeSeedService;
        private readonly IRoleDataSeedService _roleDataSeedService;
        private readonly IAccountDataSeedService _accountDataSeedService;
        private readonly IPlanTypeDataSeedService _planTypeDataSeedService;
        private readonly IUserTypeDataSeedService _userTypeDataSeedService;
        private readonly ILeadStatusDataSeedService _leadStatusDataSeedService;
        private readonly IGenderSeedService _genderSeedService;
        private readonly IDiscountTypeDataSeedService _discountTypeDataSeedService;
        private readonly IInvoiceStatusDataSeedService _invoiceStatusDataSeedService;
        private readonly IPaymentMethodDataSeedService _paymentMethodDataSeedService;
        private readonly ITimeUnitDataSeedService _timeUnitDataSeedService;
        public DataSeedService(IAccountTypeDataSeedService accountTypeSeedService,
            IRoleDataSeedService roleDataSeedService,
            IAccountDataSeedService accountDataSeedService,
            IPlanTypeDataSeedService planTypeDataSeedService,
            IUserTypeDataSeedService userTypeDataSeedService,
            ILeadStatusDataSeedService leadStatusDataSeedService,
            IGenderSeedService genderSeedService,
            IDiscountTypeDataSeedService discountTypeDataSeedService,
            IInvoiceStatusDataSeedService invoiceStatusDataSeedService,
            IPaymentMethodDataSeedService paymentMethodDataSeedService,
            ITimeUnitDataSeedService timeUnitDataSeedService)
        {
            _accountTypeSeedService = accountTypeSeedService;
            _roleDataSeedService = roleDataSeedService;
            _accountDataSeedService = accountDataSeedService;
            _planTypeDataSeedService = planTypeDataSeedService;
            _userTypeDataSeedService = userTypeDataSeedService;
            _leadStatusDataSeedService = leadStatusDataSeedService;
            _genderSeedService = genderSeedService;
            _discountTypeDataSeedService = discountTypeDataSeedService;
            _invoiceStatusDataSeedService = invoiceStatusDataSeedService;
            _paymentMethodDataSeedService = paymentMethodDataSeedService;
            _timeUnitDataSeedService = timeUnitDataSeedService;
        }

        public async Task SeedData()
        {
            // Sequence is necessary
            // Create account types e.g. unlimited, free, basic
            _accountTypeSeedService.SeedData();
            // Create roles e.g. admin, manager, user
            await _roleDataSeedService.SeedData();
            // Create super admin user with unlimited account
            await _accountDataSeedService.SeedData();
            // Create default plan types e.g. recurring, non recurring
            _planTypeDataSeedService.SeedData();
            // Create default user types e.g. Staff, Member, Lead
            _userTypeDataSeedService.SeedData();
            // Create default lead status e.g. New, Attempted, Converted
            _leadStatusDataSeedService.SeedData();
            // Create default gender e.g. male, female
            _genderSeedService.SeedData();
            // Create default discount types e.g. fixed, percentage
            _discountTypeDataSeedService.SeedData();
            // Create default invoice status e.g. draft, sent
            _invoiceStatusDataSeedService.SeedData();
            // Create default payment methods e.g. cash, card
            _paymentMethodDataSeedService.SeedData();
            // Create defualt time units e.g. day, week, month
            _timeUnitDataSeedService.SeedData();
        }
    }
}
