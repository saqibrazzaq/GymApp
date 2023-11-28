using api.Dtos.Account;
using api.Dtos.User;
using api.Utility.Paging;

namespace api.Services.Interfaces
{
    public interface IGenderService
    {
        ApiOkPagedResponse<IEnumerable<GenderRes>, MetaData> Search(GenderSearchReq dto);
    }
}
