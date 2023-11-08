using api.Dtos.PlanCategory;
using api.Services.Interfaces;
using api.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanCategoriesController : ControllerBase
    {
        private readonly IPlanCategoryService _planCategoryService;

        public PlanCategoriesController(IPlanCategoryService planCategoryService)
        {
            _planCategoryService = planCategoryService;
        }

        [HttpPost]
        [Authorize(Roles = Constants.AllAdminRoles)]
        public async Task<IActionResult> Create(PlanCategoryEditReq dto)
        {
            var res = await _planCategoryService.Create(dto);
            return Ok(res);
        }

        [HttpGet("{planCategoryId}")]
        [Authorize(Roles = Constants.AllRoles)]
        public async Task<IActionResult> Get(int planCategoryId)
        {
            var res = await _planCategoryService.Get(planCategoryId);
            return Ok(res);
        }

        [HttpPut("{planCategoryId}")]
        [Authorize(Roles = Constants.AllAdminRoles)]
        public async Task<IActionResult> Update(int planCategoryId, PlanCategoryEditReq dto)
        {
            var res = await _planCategoryService.Update(planCategoryId, dto);
            return Ok(res);
        }

        [HttpDelete("{planCategoryId}")]
        [Authorize(Roles = Constants.AllAdminRoles)]
        public async Task<IActionResult> Delete(int planCategoryId)
        {
            await _planCategoryService.Delete(planCategoryId);
            return NoContent();
        }

        [HttpGet("search")]
        [Authorize(Roles = Constants.AllRoles)]
        public async Task<IActionResult> Search([FromQuery] PlanCategorySearchReq dto)
        {
            var res = await _planCategoryService.Search(dto);
            return Ok(res);
        }
    }
}
