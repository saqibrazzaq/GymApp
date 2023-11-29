using api.Dtos.Account;
using api.Dtos.Payment;
using api.Entities;
using api.Utility.Paging;

namespace api.Repository.Interfaces
{
    public interface IPaymentMethodRepository : IRepositoryBase<PaymentMethod>
    {
        PagedList<PaymentMethod> Search(PaymentMethodSearchReq dto, bool trackChanges);
    }
}
