using api.Data;
using api.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Repository.Implementations
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly AppDbContext _context;
        private readonly Lazy<IUserRepository> _userRepository;
        private readonly Lazy<IAccountTypeRepository> _accountTypeRepository;
        private readonly Lazy<ICurrencyRepository> _currencyRepository;
        private readonly Lazy<IAccountRepository> _accountRepository;
        private readonly Lazy<ICountryRepository> _countryRepository;
        private readonly Lazy<IStateRepository> _stateRepository;
        private readonly Lazy<IAddressRepository> _addressRepository;
        private readonly Lazy<IUserAddressRepository> _userAddressRepository;
        private readonly Lazy<IPlanCategoryRepository> _planCategoryRepository;
        private readonly Lazy<IPlanTypeRepository> _planTypeRepository;
        private readonly Lazy<IPlanRepository> _planRepository;
        private readonly Lazy<IUserTypeRepository> _userTypeRepository;
        private readonly Lazy<ILeadStatusRepository> _leadStatusRepository;
        private readonly Lazy<IGenderRepository> _genderRepository;
        private readonly Lazy<IDiscountTypeRepository> _discountTypeRepository;
        private readonly Lazy<IInvoiceStatusRepository> _invoiceStatusRepository;
        public RepositoryManager(AppDbContext context)
        {
            _context = context;

            // Initialize all repositories
            _userRepository = new Lazy<IUserRepository>(() => new UserRepository(context));
            _accountTypeRepository = new Lazy<IAccountTypeRepository>(() => new AccountTypeRepository(context));
            _currencyRepository = new Lazy<ICurrencyRepository>(() => new CurrencyRepository(context));
            _accountRepository = new Lazy<IAccountRepository>(() => new AccountRepository(context));
            _countryRepository = new Lazy<ICountryRepository>(() => new CountryRepository(context));
            _stateRepository = new Lazy<IStateRepository>(() => new StateRepository(context));
            _addressRepository = new Lazy<IAddressRepository>(() => new AddressRepository(context));
            _userAddressRepository = new Lazy<IUserAddressRepository>(() => new UserAddressRepository(context));
            _planCategoryRepository = new Lazy<IPlanCategoryRepository>(() => new PlanCategoryRepository(context));
            _planTypeRepository = new Lazy<IPlanTypeRepository>(() => new PlanTypeRepository(context));
            _planRepository = new Lazy<IPlanRepository>(() => new PlanRepository(context));
            _userTypeRepository = new Lazy<IUserTypeRepository>(() => new UserTypeRepository(context));
            _leadStatusRepository = new Lazy<ILeadStatusRepository>(() => new LeadStatusRepository(context));
            _genderRepository = new Lazy<IGenderRepository>(() => new GenderRepository(context));
            _discountTypeRepository = new Lazy<IDiscountTypeRepository>(() => new DiscountTypeRepository(context));
            _invoiceStatusRepository = new Lazy<IInvoiceStatusRepository>(() => new InvoiceStatusRepository(context));
        }

        public IUserRepository UserRepository => _userRepository.Value;
        public IAccountTypeRepository AccountTypeRepository => _accountTypeRepository.Value;
        public ICurrencyRepository CurrencyRepository => _currencyRepository.Value;
        public IAccountRepository AccountRepository => _accountRepository.Value;
        public ICountryRepository CountryRepository => _countryRepository.Value;
        public IStateRepository StateRepository => _stateRepository.Value;
        public IAddressRepository AddressRepository => _addressRepository.Value;
        public IUserAddressRepository UserAddressRepository => _userAddressRepository.Value;
        public IPlanCategoryRepository PlanCategoryRepository => _planCategoryRepository.Value;
        public IPlanTypeRepository PlanTypeRepository => _planTypeRepository.Value;
        public IPlanRepository PlanRepository => _planRepository.Value;
        public IUserTypeRepository UserTypeRepository => _userTypeRepository.Value;
        public ILeadStatusRepository LeadStatusRepository => _leadStatusRepository.Value;
        public IGenderRepository GenderRepository => _genderRepository.Value;
        public IDiscountTypeRepository DiscountTypeRepository => _discountTypeRepository.Value;
        public IInvoiceStatusRepository InvoiceStatusRepository => _invoiceStatusRepository.Value;
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
