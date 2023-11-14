using api.Dtos.Plan;
using api.Dtos.User;
using api.Utility.Paging;

namespace api.Services.Interfaces
{
    public interface IUserTypeService
    {
        ApiOkPagedResponse<IEnumerable<UserTypeRes>, MetaData> Search(UserTypeSearchReq dto);
    }
}
