using api.Dtos.Invoice;
using api.Services.Interfaces;
using api.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicesController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;

        public InvoicesController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpPost]
        [Authorize(Roles = Constants.AllManagerRoles)]
        public async Task<IActionResult> Create (InvoiceEditReq dto)
        {
            var res = await _invoiceService.Create(dto);
            return Ok(res);
        }

        [HttpPost("items")]
        [Authorize(Roles = Constants.AllManagerRoles)]
        public async Task<IActionResult> CreateItem(InvoiceItemEditReq dto)
        {
            var res = await _invoiceService.CreateItem(dto);
            return Ok(res);
        }

        [HttpPut("{invoiceId}")]
        [Authorize(Roles = Constants.AllManagerRoles)]
        public async Task<IActionResult> Update(int invoiceId, InvoiceEditReq dto)
        {
            var res = await _invoiceService.Update(invoiceId, dto);
            return Ok(res);
        }

        [HttpPut("items/{invoiceItemId}")]
        [Authorize(Roles = Constants.AllManagerRoles)]
        public async Task<IActionResult> UpdateItem(int invoiceItemId, InvoiceItemEditReq dto)
        {
            var res = await _invoiceService.UpdateItem(invoiceItemId, dto);
            return Ok(res);
        }

        [HttpGet("{invoiceId}")]
        [Authorize(Roles = Constants.AllManagerRoles)]
        public async Task<IActionResult> Get(int invoiceId)
        {
            var res = await _invoiceService.Get(invoiceId);
            return Ok(res);
        }

        [HttpGet("items/invoiceItemId")]
        [Authorize(Roles = Constants.AllManagerRoles)]
        public async Task<IActionResult> GetItem(int invoiceItemId)
        {
            var res = await _invoiceService.GetItem(invoiceItemId);
            return Ok(res);
        }

        [HttpGet("search")]
        [Authorize(Roles = Constants.AllManagerRoles)]
        public async Task<IActionResult> Search([FromQuery]InvoiceSearchReq dto)
        {
            var res = await _invoiceService.Search(dto);
            return Ok(res);
        }

        [HttpGet("items/search")]
        [Authorize(Roles = Constants.AllManagerRoles)]
        public async Task<IActionResult> SearchItems([FromQuery]InvoiceItemSearchReq dto)
        {
            var res = await _invoiceService.SearchItems(dto);
            return Ok(res);
        }
    }
}
