using api.Dtos.Account;
using api.Dtos.Payment;
using api.Dtos.Plan;
using api.Services.Implementations;
using api.Services.Interfaces;
using api.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeUnitsController : ControllerBase
    {
        private readonly ITimeUnitService _timeUnitService;

        public TimeUnitsController(ITimeUnitService timeUnitService)
        {
            _timeUnitService = timeUnitService;
        }

        [HttpGet("search")]
        [Authorize(Roles = Constants.AllRoles)]
        public IActionResult Search([FromQuery] TimeUnitSearchReq dto)
        {
            var res = _timeUnitService.Search(dto);
            return Ok(res);
        }
    }
}
