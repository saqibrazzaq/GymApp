using api.Dtos.Account;
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
    public class AccountTypesController : ControllerBase
    {
        private readonly IAccountTypeService _accountTypeService;

        public AccountTypesController(IAccountTypeService accountTypeService)
        {
            _accountTypeService = accountTypeService;
        }

        [HttpGet("search")]
        [Authorize(Roles = Constants.AllRoles)]
        public IActionResult Search([FromQuery] AccountTypeSearchReq dto)
        {
            var res = _accountTypeService.Search(dto);
            return Ok(res);
        }
    }
}
