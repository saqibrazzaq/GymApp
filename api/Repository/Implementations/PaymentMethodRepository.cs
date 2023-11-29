using api.Data;
using api.Dtos.Payment;
using api.Entities;
using api.Repository.Interfaces;
using api.Utility.Paging;

namespace api.Repository.Implementations
{
    public class PaymentMethodRepository : RepositoryBase<PaymentMethod>, IPaymentMethodRepository
    {
        public PaymentMethodRepository(AppDbContext context) : base(context)
        {
        }

        public PagedList<PaymentMethod> Search(PaymentMethodSearchReq dto, bool trackChanges)
        {
            var entities = FindAll(trackChanges)
                .Search(dto)
                .Sort(dto.OrderBy)
                .Skip((dto.PageNumber - 1) * dto.PageSize)
                .Take(dto.PageSize)
                .ToList();
            var count = FindAll(trackChanges: false)
                .Search(dto)
                .Count();
            return new PagedList<PaymentMethod>(entities, count,
                dto.PageNumber, dto.PageSize);
        }
    }
}
