using api.Dtos.Account;
using api.Dtos.Payment;
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
    public class PaymentMethodsController : ControllerBase
    {
        private readonly IPaymentMethodService _paymentMethodService;

        public PaymentMethodsController(IPaymentMethodService paymentMethodService)
        {
            _paymentMethodService = paymentMethodService;
        }

        [HttpGet("search")]
        [Authorize(Roles = Constants.AllRoles)]
        public IActionResult Search([FromQuery] PaymentMethodSearchReq dto)
        {
            var res = _paymentMethodService.Search(dto);
            return Ok(res);
        }
    }
}
