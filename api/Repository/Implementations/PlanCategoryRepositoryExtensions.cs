using api.Dtos.Country;
using api.Dtos.PlanCategory;
using api.Entities;
using api.Utility.Paging;
using System.Linq.Dynamic.Core;

namespace api.Repository.Implementations
{
    public static class PlanCategoryRepositoryExtensions
    {
        public static IQueryable<PlanCategory> Search(this IQueryable<PlanCategory> items,
            PlanCategorySearchReq searchParams)
        {
            var itemsToReturn = items.Where(x => x.AccountId == searchParams.AccountId);
            
            if (string.IsNullOrWhiteSpace(searchParams.SearchText) == false)
            {
                itemsToReturn = itemsToReturn.Where(
                    x => (x.Name ?? "").ToLower().Contains(searchParams.SearchText) ||
                        (x.Name ?? "").ToLower().Contains(searchParams.SearchText)
                );
            }

            return itemsToReturn;
        }

        public static IQueryable<PlanCategory> Sort(this IQueryable<PlanCategory> items,
            string? orderBy)
        {
            if (string.IsNullOrWhiteSpace(orderBy))
                return items.OrderBy(e => e.Name);

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<PlanCategory>(orderBy);

            if (string.IsNullOrWhiteSpace(orderQuery))
                return items.OrderBy(e => e.Name);

            return items.OrderBy(orderQuery);
        }
    }
}
