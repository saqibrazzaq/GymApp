using api.Dtos.Plan;
using api.Services.Interfaces;
using api.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlansController : ControllerBase
    {
        private readonly IPlanService _planService;

        public PlansController(IPlanService planService)
        {
            _planService = planService;
        }

        [HttpPost]
        [Authorize(Roles = Constants.AllAdminRoles)]
        public async Task<IActionResult> Create(PlanEditReq dto)
        {
            var res = await _planService.Create(dto);
            return Ok(res);
        }

        [HttpPut("{planId}")]
        [Authorize(Roles = Constants.AllAdminRoles)]
        public async Task<IActionResult> Update(int planId, PlanEditReq dto)
        {
            var res = await _planService.Update(planId, dto);
            return Ok(res);
        }

        [HttpDelete("{planId}")]
        [Authorize(Roles = Constants.AllAdminRoles)]
        public async Task<IActionResult> Delete(int planId)
        {
            await _planService.Delete(planId);
            return Ok();
        }

        [HttpGet("{planId}")]
        [Authorize(Roles = Constants.AllRoles)]
        public async Task<IActionResult> Get(int planId)
        {
            var res = await _planService.Get(planId);
            return Ok(res);
        }

        [HttpGet("search")]
        [Authorize(Roles = Constants.AllRoles)]
        public async Task<IActionResult> Search([FromQuery] PlanSearchReq dto)
        {
            var res = await _planService.Search(dto);
            return Ok(res);
        }
    }
}
