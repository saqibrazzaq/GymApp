using api.Dtos.Account;
using api.Dtos.Plan;
using api.Utility.Paging;

namespace api.Services.Interfaces
{
    public interface IAccountTypeService
    {
        ApiOkPagedResponse<IEnumerable<AccountTypeRes>, MetaData> Search(AccountTypeSearchReq dto);
    }
}
