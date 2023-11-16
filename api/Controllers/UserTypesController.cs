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
    public class UserTypesController : ControllerBase
    {
        private readonly IUserTypeService _userTypeService;

        public UserTypesController(IUserTypeService userTypeService)
        {
            _userTypeService = userTypeService;
        }

        [HttpGet("search")]
        [Authorize(Roles = Constants.AllRoles)]
        public IActionResult Search([FromQuery] UserTypeSearchReq dto)
        {
            var res = _userTypeService.Search(dto);
            return Ok(res);
        }
    }
}
