using api.Dtos.Subscription;
using api.Services.Interfaces;
using api.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionsController : ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;

        public SubscriptionsController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        [HttpPost]
        [Authorize(Roles = Constants.AllAdminRoles)]
        public async Task<IActionResult> Create(SubscriptionEditReq dto)
        {
            var res = await _subscriptionService.Create(dto);
            return Ok(res);
        }

        [HttpPut("{subscriptionId}")]
        [Authorize(Roles = Constants.AllAdminRoles)]
        public async Task<IActionResult> Update(int subscriptionId, SubscriptionEditReq dto)
        {
            var res = await _subscriptionService.Update(subscriptionId, dto);
            return Ok(res);
        }

        [HttpGet("{subscriptionId}")]
        [Authorize(Roles = Constants.AllRoles)]
        public async Task<IActionResult> Get(int subscriptionId)
        {
            var res = await _subscriptionService.Get(subscriptionId);
            return Ok(res);
        }

        [HttpDelete("{subscriptionId}")]
        [Authorize(Roles = Constants.AllAdminRoles)]
        public async Task<IActionResult> Delete(int subscriptionId)
        {
            await _subscriptionService.Delete(subscriptionId);
            return NoContent();
        }

        [HttpGet("search")]
        [Authorize(Roles = Constants.AllRoles)]
        public async Task<IActionResult> Search([FromQuery] SubscriptionSearchReq dto)
        {
            var res = await _subscriptionService.Search(dto);
            return Ok(res);
        }
    }
}
