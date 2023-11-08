using api.Data;
using api.Dtos.Currency;
using api.Entities;
using api.Repository.Interfaces;
using api.Utility.Paging;

namespace api.Repository.Implementations
{
    public class CurrencyRepository : RepositoryBase<Currency>, ICurrencyRepository
    {
        public CurrencyRepository(AppDbContext context) : base(context)
        {
        }

        public PagedList<Currency> SearchCurrencies(CurrencySearchReq dto, bool trackChanges)
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
            return new PagedList<Currency>(entities, count,
                dto.PageNumber, dto.PageSize);
        }
    }
}
