using api.Dtos.Country;
using api.Dtos.Currency;
using api.Utility.Paging;

namespace api.Services.Interfaces
{
    public interface ICurrencyService
    {
        CurrencyRes Create(CurrencyEditReq dto);
        CurrencyRes Update(int currencyId, CurrencyEditReq dto);
        void Delete(int currencyId);
        CurrencyRes Get(int currencyId);
        ApiOkPagedResponse<IEnumerable<CurrencyRes>, MetaData> Search(
            CurrencySearchReq dto);
    }
}
