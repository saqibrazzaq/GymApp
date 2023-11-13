namespace api.Repository.Interfaces
{
    public interface IRepositoryManager
    {
        IUserRepository UserRepository { get; }
        IAccountTypeRepository AccountTypeRepository { get; }
        IAccountRepository AccountRepository { get; }
        ICurrencyRepository CurrencyRepository { get; }
        ICountryRepository CountryRepository { get; }
        IStateRepository StateRepository { get; }
        IAddressRepository AddressRepository { get; }
        IUserAddressRepository UserAddressRepository { get; }
        IPlanCategoryRepository PlanCategoryRepository { get; }
        IPlanTypeRepository PlanTypeRepository { get; }
        IPlanRepository PlanRepository { get; }
        void Save();
    }
}
