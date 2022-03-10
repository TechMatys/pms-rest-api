using PMS.Core.Interface.Services;
using PMS.Core.Model;
using Microsoft.AspNetCore.Mvc;

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
                return NoContent();
            }

            return Ok(response);
        }
        
        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] CompanyInvoice CompanyInvoiceModal)
        {
            return await _CompanyInvoiceService.Create(CompanyInvoiceModal);
        }

        
        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> Delete(int id)
        {
            return await _CompanyInvoiceService.Delete(id);
        }
    }
}
