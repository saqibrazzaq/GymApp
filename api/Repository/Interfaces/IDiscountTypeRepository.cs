using api.Dtos.Account;
using api.Dtos.Invoice;
using api.Entities;
using api.Utility.Paging;

namespace api.Repository.Interfaces
{
    public interface IDiscountTypeRepository : IRepositoryBase<DiscountType>
    {
        PagedList<DiscountType> Search(DiscountTypeSearchReq dto, bool trackChanges);
    }
}
