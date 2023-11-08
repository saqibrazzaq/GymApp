using api.Dtos.Currency;
using api.Services.Interfaces;
using api.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrenciesController : ControllerBase
    {
        private readonly ICurrencyService _currencyService;

        public CurrenciesController(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        [HttpPost]
        [Authorize(Roles = Constants.SuperAdminRole)]
        public IActionResult Create(CurrencyEditReq dto)
        {
            var res = _currencyService.Create(dto);
            return Ok(res);
        }

        [HttpGet("{currencyId}")]
        [Authorize(Roles = Constants.AllRoles)]
        public IActionResult Get(int currencyId)
        {
            var res = _currencyService.Get(currencyId);
            return Ok(res);
        }

        [HttpPut("{currencyId}")]
        [Authorize(Roles = Constants.SuperAdminRole)]
        public IActionResult Update(int currencyId, CurrencyEditReq dto)
        {
            var res = _currencyService.Update(currencyId, dto);
            return Ok(res);
        }

        [HttpDelete("{currencyId}")]
        public IActionResult Delete(int currencyId)
        {
            _currencyService.Delete(currencyId);
            return NoContent();
        }

        [HttpGet("search")]
        [Authorize(Roles = Constants.AllRoles)]
        public IActionResult Search([FromQuery] CurrencySearchReq dto)
        {
            var res = _currencyService.Search(dto);
            return Ok(res);
        }
    }
}
