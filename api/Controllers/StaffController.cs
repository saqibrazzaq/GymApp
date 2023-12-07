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
    public class StaffController : ControllerBase
    {
        private readonly IStaffService _staffService;

        public StaffController(IStaffService userService)
        {
            _staffService = userService;
        }

        [HttpGet("search")]
        [Authorize(Roles = Constants.AllAdminRoles)]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> SearchUsers(
            [FromQuery] SearchUsersReq dto)
        {
            var res = await _staffService.SearchStaff(
                dto, trackChanges: false);
            return Ok(res);
        }

        [HttpPost]
        [Authorize(Roles = Constants.AllAdminRoles)]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateStaff(
            [FromBody] StaffCreateReq dto)
        {
            await _staffService.CreateStaff(dto);
            return Ok();
        }

        [HttpPut("{email}")]
        [Authorize(Roles = Constants.AllAdminRoles)]
        public async Task<IActionResult> UpdateStaff(string email, UserEditReq dto)
        {
            await _staffService.UpdateUser(email, dto);
            return Ok();
        }

        [HttpPost("add-role")]
        [Authorize(Roles = Constants.AllAdminRoles)]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> AddRoleToStaff(
            [FromBody] AddRoleReq dto)
        {
            await _staffService.AddRoleToStaff(dto);
            return Ok();
        }

        [HttpDelete("remove-role")]
        [Authorize(Roles = Constants.AllAdminRoles)]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> RemoveRoleFromStaff(
            [FromBody] RemoveRoleReq dto)
        {
            await _staffService.RemoveRoleFromStaff(dto);
            return Ok();
        }

        [HttpDelete("{username}")]
        [Authorize(Roles = Constants.AllAdminRoles)]
        public async Task<IActionResult> DeleteStaff(
            string username)
        {
            await _staffService.Delete(new DeleteUserReq(
                username));
            return Ok();
        }

        [HttpGet("get/{username}")]
        [Authorize(Roles = Constants.AllAdminRoles)]
        public async Task<IActionResult> GetStaff(
            string username)
        {
            var res = await _staffService.FindByUsername(username);
            return Ok(res);
        }

        [HttpGet("roles")]
        [Authorize(Roles =Constants.AllAdminRoles)]
        public IActionResult GetAllRoles()
        {
            var res = _staffService.GetAllRoles();
            return Ok(res);
        }

        [HttpPost("{email}/update-profile-picture")]
        [Authorize(Roles = Constants.AllRoles)]
        public async Task<IActionResult> UpdateProfilePicture(string email)
        {
            await _staffService.UpdateProfilePicture(email, Request.Form.Files[0]);
            return NoContent();
        }

        [HttpPost("{email}/set-new-password")]
        [Authorize(Roles = Constants.AllAdminRoles)]
        public async Task<IActionResult> SetNewPassword(string email, SetNewPasswordReq dto)
        {
            await _staffService.SetNewPassword(email, dto);
            return NoContent();
        }
    }
}
