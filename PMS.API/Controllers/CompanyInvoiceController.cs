using PMS.Core.Interface.Services;
using PMS.Core.Model;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using PMS.Core.Services;

namespace PMS.API.Controllers
{
    [Route("company-invoice")]
    [ApiController]
    public class CompanyInvoiceController : ControllerBase
    {
        private readonly ICompanyInvoiceService _CompanyInvoiceService;

        public CompanyInvoiceController(ICompanyInvoiceService CompanyInvoiceService)
        {
            _CompanyInvoiceService = CompanyInvoiceService ?? throw new ArgumentNullException(nameof(CompanyInvoiceService));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompanyInvoiceListModel>>> GetAllCompanyInvoice()
        {
            var response = await _CompanyInvoiceService.GetAllCompanyInvoice();
            if (response == null)
            {
                return Ok(new
                {
                    message = "Server Error",
                    statusCode = HttpStatusCode.InternalServerError
                });
            }
            return Ok(new
            {
                response,
                statusCode = HttpStatusCode.OK
            });
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] CompanyInvoice CompanyInvoiceModal)
        {
            var response = await _CompanyInvoiceService.Create(CompanyInvoiceModal);
            if (response == null)
            {
                return Ok(new
                {
                    message = "Server Error",
                    StatusCode = HttpStatusCode.InternalServerError,
                });
            }
            return Ok(new
            {
                response,
                message = "Created",
                statusCode = HttpStatusCode.OK,
            });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> Delete(int id)
        {
            var response = await _CompanyInvoiceService.Delete(id);
            if (response == null)
            {
                return Ok(new
                {
                    message = "Server Error",
                    StatusCode = HttpStatusCode.InternalServerError,
                });
            }
            if (response < 1)
            {
                return Ok(new
                {
                    message = "Record not found",
                    statusCode = HttpStatusCode.NotFound,
                });
            }
            return Ok(new
            {
                message = "Deleted",
                statusCode = HttpStatusCode.OK
            });
        }
    }
}
