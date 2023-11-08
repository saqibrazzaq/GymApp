using api.Dtos.Account;
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
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("my")]
        [Authorize(Roles = Constants.AllRoles)]
        public async Task<IActionResult> GetMyAccount()
        {
            var res = await _accountService.GetMyAccount();
            return Ok(res);
        }

        [HttpPost("update-logo")]
        [Authorize(Roles = Constants.AllAdminRoles)]
        public async Task<IActionResult> UpdateLogo()
        {
            await _accountService.UpdateLogo(Request.Form.Files[0]);
            return NoContent();
        }

        [HttpPut]
        [Authorize(Roles = Constants.AllAdminRoles)]
        public async Task<IActionResult> Update(AccountEditReq dto)
        {
            var res = await _accountService.Update(dto);
            return Ok(res);
        }
    }
}
