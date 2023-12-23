using api.Dtos.Country;
using api.Dtos.Invoice;
using api.Dtos.Plan;
using api.Dtos.PlanCategory;
using api.Entities;
using api.Utility.Paging;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace api.Repository.Implementations
{
    public static class InvoiceRepositoryExtensions
    {
        public static IQueryable<Invoice> Search(this IQueryable<Invoice> items,
            InvoiceSearchReq searchParams)
        {
            var itemsToReturn = items
                .Include(x => x.State)
                .Include(x => x.Status)
                .Include(x => x.User)
                .Where(x => x.AccountId == searchParams.AccountId)
                ;
            
            if (string.IsNullOrWhiteSpace(searchParams.SearchText) == false)
            {
                //itemsToReturn = itemsToReturn.Where(
                //    x => (x.Name ?? "").ToLower().Contains(searchParams.SearchText) ||
                //        (x.Name ?? "").ToLower().Contains(searchParams.SearchText)
                //);
            }

            return itemsToReturn;
        }

        public static IQueryable<Invoice> Sort(this IQueryable<Invoice> items,
            string? orderBy)
        {
            if (string.IsNullOrWhiteSpace(orderBy))
                return items.OrderByDescending(e => e.IssueDate);

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Invoice>(orderBy);

            if (string.IsNullOrWhiteSpace(orderQuery))
                return items.OrderByDescending(e => e.IssueDate);

            return items.OrderBy(orderQuery);
        }
    }
}
