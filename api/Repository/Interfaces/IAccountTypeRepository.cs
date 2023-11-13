using api.Dtos.Account;
using api.Dtos.Plan;
using api.Entities;
using api.Utility.Paging;

namespace api.Repository.Interfaces
{
    public interface IAccountTypeRepository : IRepositoryBase<AccountType>
    {
        PagedList<AccountType> Search(AccountTypeSearchReq dto, bool trackChanges);
    }
}
