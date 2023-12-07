using api.Dtos.Country;
using api.Dtos.Plan;
using api.Dtos.PlanCategory;
using api.Dtos.Subscription;
using api.Entities;
using api.Utility.Paging;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace api.Repository.Implementations
{
    public static class SubscriptionRepositoryExtensions
    {
        public static IQueryable<Subscription> Search(this IQueryable<Subscription> items,
            SubscriptionSearchReq searchParams)
        {
            var itemsToReturn = items
                .Include(x => x.Plan)
                .Include(x => x.User)
                .Where(x => x.User.AccountId == searchParams.AccountId)
                ;
            
            if (string.IsNullOrWhiteSpace(searchParams.SearchText) == false)
            {
                //itemsToReturn = itemsToReturn.Where(
                //    x => (x.Name ?? "").ToLower().Contains(searchParams.SearchText) ||
                //        (x.Name ?? "").ToLower().Contains(searchParams.SearchText)
                //);
            }

            if (searchParams.PlanId.HasValue && searchParams.PlanId > 0)
            {
                itemsToReturn = itemsToReturn.Where(
                    x => x.PlanId == searchParams.PlanId);
            }

            if (string.IsNullOrWhiteSpace(searchParams.Email) == false)
            {
                itemsToReturn = itemsToReturn.Where(
                    x => x.User.Email.Equals(searchParams.Email));
            }

            return itemsToReturn;
        }

        public static IQueryable<Subscription> Sort(this IQueryable<Subscription> items,
            string? orderBy)
        {
            if (string.IsNullOrWhiteSpace(orderBy))
                return items.OrderByDescending(e => e.ActiveTo);

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Subscription>(orderBy);

            if (string.IsNullOrWhiteSpace(orderQuery))
                return items.OrderByDescending(e => e.ActiveTo);

            return items.OrderBy(orderQuery);
        }
    }
}
