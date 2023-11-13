using api.Dtos.Account;
using api.Dtos.Country;
using api.Dtos.Plan;
using api.Dtos.PlanCategory;
using api.Entities;
using api.Utility.Paging;
using System.Linq.Dynamic.Core;

namespace api.Repository.Implementations
{
    public static class AccountTypeRepositoryExtensions
    {
        public static IQueryable<AccountType> Search(this IQueryable<AccountType> items,
            AccountTypeSearchReq searchParams)
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

        public static IQueryable<AccountType> Sort(this IQueryable<AccountType> items,
            string? orderBy)
        {
            if (string.IsNullOrWhiteSpace(orderBy))
                return items.OrderBy(e => e.Name);

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<AccountType>(orderBy);

            if (string.IsNullOrWhiteSpace(orderQuery))
                return items.OrderBy(e => e.Name);

            return items.OrderBy(orderQuery);
        }
    }
}
