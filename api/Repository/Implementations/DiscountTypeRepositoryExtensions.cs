using api.Dtos.Account;
using api.Dtos.Invoice;
using api.Entities;
using api.Utility.Paging;
using System.Linq.Dynamic.Core;

namespace api.Repository.Implementations
{
    public static class DiscountTypeRepositoryExtensions
    {
        public static IQueryable<DiscountType> Search(this IQueryable<DiscountType> items,
            DiscountTypeSearchReq searchParams)
        {
            var itemsToReturn = items;

            if (string.IsNullOrWhiteSpace(searchParams.SearchText) == false)
            {
                itemsToReturn = itemsToReturn.Where(
                    x => (x.Name ?? "").ToLower().Contains(searchParams.SearchText) ||
                        (x.Name ?? "").ToLower().Contains(searchParams.SearchText)
                );
            }

            return itemsToReturn;
        }

        public static IQueryable<DiscountType> Sort(this IQueryable<DiscountType> items,
            string? orderBy)
        {
            if (string.IsNullOrWhiteSpace(orderBy))
                return items.OrderBy(e => e.Name);

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<DiscountType>(orderBy);

            if (string.IsNullOrWhiteSpace(orderQuery))
                return items.OrderBy(e => e.Name);

            return items.OrderBy(orderQuery);
        }
    }
}
