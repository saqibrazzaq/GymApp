using api.Dtos.User;
using api.Entities;
using api.Utility.Paging;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace api.Repository.Implementations
{
    public static class UserRepositoryExtension
    {
        public static IQueryable<AppIdentityUser> SearchStaff(this IQueryable<AppIdentityUser> items,
            SearchUsersReq searchParams)
        {
            var itemsToReturn = items;

            // Must match account id
            itemsToReturn = itemsToReturn.Where(
                x => x.AccountId == searchParams.AccountId &&
                x.UserTypeId == (int)UserTypeNames.Staff);

            if (string.IsNullOrWhiteSpace(searchParams.SearchText) == false)
            {
                itemsToReturn = itemsToReturn.Where(
                    x => (x.UserName ?? "").ToLower().Contains(searchParams.SearchText) ||
                        x.Email.Contains(searchParams.SearchText)
                );
            }

            return itemsToReturn;
        }

        public static IQueryable<AppIdentityUser> SortStaff(this IQueryable<AppIdentityUser> users,
            string? orderBy)
        {
            if (string.IsNullOrWhiteSpace(orderBy))
                return users.OrderBy(e => e.UserName);

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<AppIdentityUser>(orderBy);

            if (string.IsNullOrWhiteSpace(orderQuery))
                return users.OrderBy(e => e.UserName);

            return users.OrderBy(orderQuery);
        }

        public static IQueryable<AppIdentityUser> SearchMembers(this IQueryable<AppIdentityUser> items,
            SearchUsersReq searchParams)
        {
            var itemsToReturn = items;

            // Must match account id
            itemsToReturn = itemsToReturn.Where(
                x => x.AccountId == searchParams.AccountId &&
                x.UserTypeId == (int)UserTypeNames.Member);

            if (string.IsNullOrWhiteSpace(searchParams.SearchText) == false)
            {
                itemsToReturn = itemsToReturn.Where(
                    x => (x.UserName ?? "").ToLower().Contains(searchParams.SearchText) ||
                        x.Email.Contains(searchParams.SearchText)
                );
            }

            return itemsToReturn;
        }

        public static IQueryable<AppIdentityUser> SortMembers(this IQueryable<AppIdentityUser> users,
            string? orderBy)
        {
            if (string.IsNullOrWhiteSpace(orderBy))
                return users.OrderBy(e => e.UserName);

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<AppIdentityUser>(orderBy);

            if (string.IsNullOrWhiteSpace(orderQuery))
                return users.OrderBy(e => e.UserName);

            return users.OrderBy(orderQuery);
        }
    }
}
