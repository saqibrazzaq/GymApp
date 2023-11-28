using api.Dtos.Account;
using api.Dtos.Invoice;
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
    public class DiscountTypesController : ControllerBase
    {
        private readonly IDiscountTypeService _discountTypeService;

        public DiscountTypesController(IDiscountTypeService discountTypeService)
        {
            _discountTypeService = discountTypeService;
        }

        [HttpGet("search")]
        [Authorize(Roles = Constants.AllRoles)]
        public IActionResult Search([FromQuery] DiscountTypeSearchReq dto)
        {
            var res = _discountTypeService.Search(dto);
            return Ok(res);
        }
    }
}
