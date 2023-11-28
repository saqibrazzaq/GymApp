using api.Dtos.Account;
using api.Dtos.Invoice;
using api.Utility.Paging;

namespace api.Services.Interfaces
{
    public interface IDiscountTypeService
    {
        ApiOkPagedResponse<IEnumerable<DiscountTypeRes>, MetaData> Search(DiscountTypeSearchReq dto);
    }
}
