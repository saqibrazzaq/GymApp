using api.Entities;
using api.Repository.Interfaces;
using api.Services.Interfaces;

namespace api.Services.Implementations
{
    public class AccountTypeDataSeedService : IAccountTypeDataSeedService
    {
        private readonly IRepositoryManager _rep;

        public AccountTypeDataSeedService(IRepositoryManager rep)
        {
            _rep = rep;
        }

        public void SeedData()
        {
            int rowsAdded = 0;

            // Read all from enum
            var accountTypes = Enum.GetValues(typeof(AccountTypeNames)).Cast<AccountTypeNames>();
            foreach (var accountType in accountTypes)
            {
                var entity = _rep.AccountTypeRepository.FindByCondition(
                    x => x.AccountTypeId == (int)accountType, false).FirstOrDefault();
                if (entity == null)
                {
                    _rep.AccountTypeRepository.Create(new AccountType
                    {
                        AccountTypeId = (int)accountType,
                        Name = Enum.GetName(typeof(AccountTypeNames), accountType)
                    });
                    rowsAdded++;
                }
            }

            if (rowsAdded > 0)
            {
                _rep.Save();
            }
        }
    }
}
