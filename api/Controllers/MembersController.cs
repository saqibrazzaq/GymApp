using api.Common.ActionFilters;
using api.Dtos.User;
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
    public class MembersController : ControllerBase
    {
        private readonly IMemberService _memberService;

        public MembersController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        [HttpGet("search")]
        [Authorize(Roles = Constants.AllAdminRoles)]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> SearchMembers(
            [FromQuery] SearchUsersReq dto)
        {
            var res = await _memberService.SearchMembers(
                dto, trackChanges: false);
            return Ok(res);
        }

        [HttpPost]
        [Authorize(Roles = Constants.AllAdminRoles)]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateMember(
            [FromBody] StaffCreateReq dto)
        {
            await _memberService.CreateMember(dto);
            return Ok();
        }

        [HttpPut("{email}")]
        [Authorize(Roles = Constants.AllAdminRoles)]
        public async Task<IActionResult> UpdateMember(string email, UserEditReq dto)
        {
            await _memberService.UpdateUser(email, dto);
            return Ok();
        }

        [HttpDelete("{username}")]
        [Authorize(Roles = Constants.AllAdminRoles)]
        public async Task<IActionResult> DeleteMember(
            string username)
        {
            await _memberService.Delete(new DeleteUserReq(
                username));
            return Ok();
        }

        [HttpGet("get/{username}")]
        [Authorize(Roles = Constants.AllAdminRoles)]
        public async Task<IActionResult> GetMember(
            string username)
        {
            var res = await _memberService.FindByUsername(username);
            return Ok(res);
        }

        [HttpPost("{email}/update-profile-picture")]
        [Authorize(Roles = Constants.AllRoles)]
        public async Task<IActionResult> UpdateProfilePicture(string email)
        {
            await _memberService.UpdateProfilePicture(email, Request.Form.Files[0]);
            return NoContent();
        }

        [HttpPost("{email}/set-new-password")]
        [Authorize(Roles = Constants.AllAdminRoles)]
        public async Task<IActionResult> SetNewPassword(string email, SetNewPasswordReq dto)
        {
            await _memberService.SetNewPassword(email, dto);
            return NoContent();
        }
    }
}
