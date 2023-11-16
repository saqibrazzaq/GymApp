using api.Dtos.User;
using api.Services.Interfaces;
using api.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeadStatusesController : ControllerBase
    {
        private readonly ILeadStatusService _leadStatusService;

        public LeadStatusesController(ILeadStatusService leadStatusService)
        {
            _leadStatusService = leadStatusService;
        }

        [HttpGet("search")]
        [Authorize(Roles = Constants.AllRoles)]
        public IActionResult Search([FromQuery] LeadStatusSearchReq dto)
        {
            var res = _leadStatusService.Search(dto);
            return Ok(res);
        }
    }
}
