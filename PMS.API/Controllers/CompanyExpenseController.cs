using PMS.Core.Interface.Services;
using PMS.Core.Model;
using Microsoft.AspNetCore.Mvc;

namespace PMS.API.Controllers
{

    [Route("company-expense")]
    [ApiController]
    public class CompanyExpenseController : ControllerBase
    { 
        private readonly ICompanyExpenseService _CompanyExpenseService;

        public CompanyExpenseController(ICompanyExpenseService CompanyExpenseService)
        {
            _CompanyExpenseService = CompanyExpenseService ?? throw new ArgumentNullException(nameof(CompanyExpenseService));
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompanyExpenseListModel>>> GetAllCompanyExpense()
        {
            var response = await _CompanyExpenseService.GetAllCompanyExpenses();

            if (response == null)
            {
                return NoContent();
            }

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CompanyExpense>> GetCompanyExpenseById(int id)
        {
            var response = await _CompanyExpenseService.GetCompanyExpenseById(id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<bool>> Create([FromBody] CompanyExpense CompanyExpenseModal)
        {
            return await _CompanyExpenseService.Create(CompanyExpenseModal);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<bool>> Update(int id, [FromBody] CompanyExpense CompanyExpenseModal)
        {
            return await _CompanyExpenseService.Update(id, CompanyExpenseModal);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            return await _CompanyExpenseService.Delete(id);
        }
    }
}
