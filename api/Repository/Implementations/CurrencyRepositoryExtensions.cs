using api.Dtos.Country;
using api.Dtos.Currency;
using api.Entities;
using api.Utility.Paging;
using System.Linq.Dynamic.Core;

namespace api.Repository.Implementations
{
    public static class CurrencyRepositoryExtensions
    {
        public static IQueryable<Currency> Search(this IQueryable<Currency> items,
            CurrencySearchReq searchParams)
        {
            var itemsToReturn = items;

            if (string.IsNullOrWhiteSpace(searchParams.SearchText) == false)
            {
                itemsToReturn = itemsToReturn.Where(
                    x => (x.Name ?? "").ToLower().Contains(searchParams.SearchText) ||
                        (x.Code ?? "").ToLower().Contains(searchParams.SearchText)
                );
            }

            return itemsToReturn;
        }

        public static IQueryable<Currency> Sort(this IQueryable<Currency> items,
            string? orderBy)
        {
            if (string.IsNullOrWhiteSpace(orderBy))
                return items.OrderBy(e => e.Name);

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Currency>(orderBy);

            if (string.IsNullOrWhiteSpace(orderQuery))
                return items.OrderBy(e => e.Name);

            return items.OrderBy(orderQuery);
        }
    }
}
