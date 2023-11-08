using api.Dtos.PlanCategory;
using api.Dtos.User;
using api.Entities;
using api.Utility.Paging;

namespace api.Repository.Interfaces
{
    public interface IPlanCategoryRepository : IRepositoryBase<PlanCategory>
    {
        PagedList<PlanCategory> Search(
            PlanCategorySearchReq dto, bool trackChanges);
    }
}
