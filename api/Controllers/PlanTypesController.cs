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
    public class PlanTypesController : ControllerBase
    {
        private readonly IPlanTypeService _planTypeService;

        public PlanTypesController(IPlanTypeService planTypeService)
        {
            _planTypeService = planTypeService;
        }

        [HttpGet("search")]
        [Authorize(Roles = Constants.AllRoles)]
        public IActionResult Search([FromQuery]PlanTypeSearchReq dto)
        {
            var res = _planTypeService.Search(dto);
            return Ok(res);
        }
    }
}
