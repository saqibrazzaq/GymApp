using api.Common;
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
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Register(
            [FromBody] StaffCreateReq dto)
        {
            await _authService.RegisterOwner(dto);
            return Ok();
        }

        [HttpPost("login")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Login([FromBody] LoginReq dto)
        {
            var res = await _authService.Login(dto);
            setRefreshTokenCookie(res.RefreshToken);
            return Ok(res);
        }

        [HttpPost("refresh-token")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> RefreshToken(
            [FromBody] TokenRes dto)
        {
            //dto.RefreshToken = Request.Cookies[Constants.RefreshTokenCookieName];
            var res = await _authService.RefreshToken(dto);
            setRefreshTokenCookie(res.RefreshToken);

            return Ok(res);
        }

        [HttpGet("send-forgot-password-email")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> SendForgotPasswordEmail(
            [FromQuery] SendForgotPasswordEmailReq dto)
        {
            await _authService.SendForgotPasswordEmail(dto);
            return Ok();
        }

        [HttpPost("reset-password")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> ResetPassword
            ([FromBody] ResetPasswordReq dto)
        {
            await _authService.ResetPassword(dto);
            return Ok();
        }

        private void setRefreshTokenCookie(string? refreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTimeOffset.UtcNow.AddDays(int.Parse(
                    SecretUtility.JWTRefreshTokenValidityInMinutes)),
                SameSite = SameSiteMode.None,
                Secure = true
            };

            // Delete the refresh token cookie, if no token is passed
            if (string.IsNullOrEmpty(refreshToken))
            {
                Response.Cookies.Delete(Constants.RefreshTokenCookieName);
            }
            else
            {
                // Set the refresh token cookie
                Response.Cookies.Append(Constants.RefreshTokenCookieName,
                    refreshToken, cookieOptions);
            }

        }
    }
}
