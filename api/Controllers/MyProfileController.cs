using api.Common.ActionFilters;
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
    public class MyProfileController : ControllerBase
    {
        private readonly IMyProfileService _myProfileService;

        public MyProfileController(IMyProfileService myProfileService)
        {
            _myProfileService = myProfileService;
        }

        [HttpGet("info")]
        [Authorize(Roles = Constants.AllRoles)]
        public async Task<IActionResult> GetUser()
        {
            var res = await _myProfileService.GetLoggedInUser();
            return Ok(res);
        }

        [HttpPost("change-password")]
        [Authorize(Roles = Constants.AllRoles)]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> ChangePassword(
            [FromBody] ChangePasswordReq dto)
        {
            await _myProfileService.ChangePassword(dto);
            return Ok();
        }

        [HttpPost("verify-email")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> VerifyEmail([FromBody] VerifyEmailReq dto)
        {
            await _myProfileService.VerifyEmail(dto);
            return Ok();
        }

        [HttpGet("send-verification-email")]
        [Authorize(Roles = Constants.AllRoles)]
        public async Task<IActionResult> SendVerificationEmail()
        {
            await _myProfileService.SendVerificationEmail();
            return Ok("Verification email sent.");
        }

        [HttpPost("update-profile-picture")]
        [Authorize(Roles = Constants.AllRoles)]
        public async Task<IActionResult> UpdateProfilePicture()
        {
            await _myProfileService.UpdateProfilePicture(Request.Form.Files[0]);
            return NoContent();
        }
    }
}
