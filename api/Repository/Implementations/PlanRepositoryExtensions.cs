using api.Dtos.Country;
using api.Dtos.Plan;
using api.Dtos.PlanCategory;
using api.Entities;
using api.Utility.Paging;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace api.Repository.Implementations
{
    public static class PlanRepositoryExtensions
    {
        public static IQueryable<Plan> Search(this IQueryable<Plan> items,
            PlanSearchReq searchParams)
        {
            var itemsToReturn = items
                .Include(x => x.PlanCategory)
                .Include(x => x.PlanType)
                .Include(x => x.TimeUnit)
                .Where(x => x.AccountId == searchParams.AccountId)
                ;
            
            if (string.IsNullOrWhiteSpace(searchParams.SearchText) == false)
            {
                itemsToReturn = itemsToReturn.Where(
                    x => (x.Name ?? "").ToLower().Contains(searchParams.SearchText) ||
                        (x.Name ?? "").ToLower().Contains(searchParams.SearchText)
                );
            }

            return itemsToReturn;
        }

        public static IQueryable<Plan> Sort(this IQueryable<Plan> items,
            string? orderBy)
        {
            if (string.IsNullOrWhiteSpace(orderBy))
                return items.OrderBy(e => e.Name);

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Plan>(orderBy);

            if (string.IsNullOrWhiteSpace(orderQuery))
                return items.OrderBy(e => e.Name);

            return items.OrderBy(orderQuery);
        }
    }
}
