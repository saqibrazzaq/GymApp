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
    public static class InvoiceItemRepositoryExtensions
    {
        public static IQueryable<InvoiceItem> Search(this IQueryable<InvoiceItem> items,
            InvoiceItemSearchReq searchParams)
        {
            var itemsToReturn = items
                .Where(x => x.InvoiceId == searchParams.InvoiceId &&
                x.Invoice.AccountId == searchParams.AccountId)
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

        public static IQueryable<InvoiceItem> Sort(this IQueryable<InvoiceItem> items,
            string? orderBy)
        {
            if (string.IsNullOrWhiteSpace(orderBy))
                return items.OrderBy(e => e.InvoiceItemId);

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<InvoiceItem>(orderBy);

            if (string.IsNullOrWhiteSpace(orderQuery))
                return items.OrderBy(e => e.InvoiceItemId);

            return items.OrderBy(orderQuery);
        }
    }
}
