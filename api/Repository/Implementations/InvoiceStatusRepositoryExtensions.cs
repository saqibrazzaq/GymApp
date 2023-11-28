using api.Dtos.Account;
using api.Dtos.Country;
using api.Dtos.Invoice;
using api.Dtos.Plan;
using api.Dtos.PlanCategory;
using api.Entities;
using api.Utility.Paging;
using System.Linq.Dynamic.Core;

namespace api.Repository.Implementations
{
    public static class InvoiceStatusRepositoryExtensions
    {
        public static IQueryable<InvoiceStatus> Search(this IQueryable<InvoiceStatus> items,
            InvoiceStatusSearchReq searchParams)
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

        public static IQueryable<InvoiceStatus> Sort(this IQueryable<InvoiceStatus> items,
            string? orderBy)
        {
            if (string.IsNullOrWhiteSpace(orderBy))
                return items.OrderBy(e => e.Name);

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<InvoiceStatus>(orderBy);

            if (string.IsNullOrWhiteSpace(orderQuery))
                return items.OrderBy(e => e.Name);

            return items.OrderBy(orderQuery);
        }
    }
}
