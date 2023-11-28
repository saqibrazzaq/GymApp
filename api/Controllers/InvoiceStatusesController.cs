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
    public class InvoiceStatusesController : ControllerBase
    {
        private readonly IInvoiceStatusService _invoiceStatusService;

        public InvoiceStatusesController(IInvoiceStatusService invoiceStatusService)
        {
            _invoiceStatusService = invoiceStatusService;
        }

        [HttpGet("search")]
        [Authorize(Roles = Constants.AllRoles)]
        public IActionResult Search([FromQuery] InvoiceStatusSearchReq dto)
        {
            var res = _invoiceStatusService.Search(dto);
            return Ok(res);
        }
    }
}
