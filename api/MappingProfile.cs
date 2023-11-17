﻿using api.Dtos.Account;
using api.Dtos.Address;
using api.Dtos.Country;
using api.Dtos.Currency;
using api.Dtos.Plan;
using api.Dtos.PlanCategory;
using api.Dtos.User;
using api.Entities;

namespace api
{
    public class MappingProfile : AutoMapper.Profile
    {
        public MappingProfile()
        {
            // User
            CreateMap<AppIdentityUser, AuthenticationRes>();
            CreateMap<AppIdentityUser, UserRes>();
            CreateMap<StaffCreateReq, AppIdentityUser>();
            CreateMap<StaffEditReq, AppIdentityUser>();

            // Account
            CreateMap<Account, AccountRes>();
            CreateMap<AccountEditReq, Account>();

            // AccountType
            CreateMap<AccountType, AccountTypeRes>();

            // Currency
            CreateMap<Currency, CurrencyRes>();
            CreateMap<CurrencyEditReq, Currency>();

            // Country
            CreateMap<Country, CountryRes>();
            CreateMap<Country, CountryWithStateCountRes>();
            CreateMap<CountryEditReq, Country>();

            // State
            CreateMap<State, StateRes>();
            CreateMap<StateEditReq, State>();

            // Address
            CreateMap<Address, AddressRes>();
            CreateMap<AddressEditReq, Address>();
            CreateMap<UserAddress, UserAddressRes>();
            CreateMap<UserAddressEditReq, UserAddress>();

            // PlanCategory
            CreateMap<PlanCategory, PlanCategoryRes>();
            CreateMap<PlanCategoryEditReq, PlanCategory>();

            // PlanType
            CreateMap<PlanType, PlanTypeRes>();

            // Plan
            CreateMap<Plan, PlanRes>();
            CreateMap<PlanEditReq, Plan>();

            // UserType
            CreateMap<UserType, UserTypeRes>();

            // LeadStatus
            CreateMap<LeadStatus, LeadStatusRes>();
        }
    }
}
