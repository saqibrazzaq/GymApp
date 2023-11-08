using api.Dtos.Country;
using api.Dtos.PlanCategory;
using api.Utility.Paging;

namespace api.Services.Interfaces
{
    public interface IPlanCategoryService
    {
        Task<PlanCategoryRes> Create(PlanCategoryEditReq dto);
        Task<PlanCategoryRes> Update(int planCategoryId, PlanCategoryEditReq dto);
        Task Delete(int planCategoryId);
        Task<PlanCategoryRes> Get(int planCategoryId);
        Task<ApiOkPagedResponse<IEnumerable<PlanCategoryRes>, MetaData>> Search(
            PlanCategorySearchReq dto);
    }
}
