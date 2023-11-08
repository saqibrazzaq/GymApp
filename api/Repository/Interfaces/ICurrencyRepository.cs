using api.Dtos.Currency;
using api.Entities;
using api.Utility.Paging;

namespace api.Repository.Interfaces
{
    public interface ICurrencyRepository : IRepositoryBase<Currency>
    {
        PagedList<Currency> SearchCurrencies(CurrencySearchReq dto, bool trackChanges);
    }
}
