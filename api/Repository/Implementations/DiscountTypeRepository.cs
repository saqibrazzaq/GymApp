using api.Data;
using api.Dtos.Invoice;
using api.Entities;
using api.Repository.Interfaces;
using api.Utility.Paging;

namespace api.Repository.Implementations
{
    public class DiscountTypeRepository : RepositoryBase<DiscountType>, IDiscountTypeRepository
    {
        public DiscountTypeRepository(AppDbContext context) : base(context)
        {
        }

        public PagedList<DiscountType> Search(DiscountTypeSearchReq dto, bool trackChanges)
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
            return new PagedList<DiscountType>(entities, count,
                dto.PageNumber, dto.PageSize);
        }
    }
}
