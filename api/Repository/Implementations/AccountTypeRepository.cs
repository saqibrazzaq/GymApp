using api.Data;
using api.Dtos.Account;
using api.Entities;
using api.Repository.Interfaces;
using api.Utility.Paging;

namespace api.Repository.Implementations
{
    public class AccountTypeRepository : RepositoryBase<AccountType>, IAccountTypeRepository
    {
        public AccountTypeRepository(AppDbContext context) : base(context)
        {
        }

        public PagedList<AccountType> Search(AccountTypeSearchReq dto, bool trackChanges)
        {
            var entities = FindAll(trackChanges)
                .Search(dto)
                .Sort(dto.OrderBy)
                .Skip((dto.PageNumber - 1) * dto.PageSize)
                .Take(dto.PageSize)
                .ToList();
            var count = FindAll(trackChanges: false)
                .Search(dto)
                .Count();
            return new PagedList<AccountType>(entities, count,
                dto.PageNumber, dto.PageSize);
        }
    }
}
