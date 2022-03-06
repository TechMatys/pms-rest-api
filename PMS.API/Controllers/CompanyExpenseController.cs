using PMS.Core.Interface.Services;
using PMS.Core.Model;
using Microsoft.AspNetCore.Mvc;

namespace PMS.API.Controllers
{

    [Route("company-expense")]
    [ApiController]
    public class CompanyExpenseController : ControllerBase
    { 
    {
        private readonly ICompanyExpenseService _CompanyExpenseGroupService;

        public CompanyExpenseController(ICompanyExpenseService CompanyExpenseGroupService)
        {
            _CompanyExpenseGroupService = CompanyExpenseGroupService ?? throw new ArgumentNullException(nameof(CompanyExpenseGroupService));
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompanyExpenseListModel>>> GetAllCompanyExpense()
        {
            var response = await _CompanyExpenseGroupService.GetAllCompanyExpense();

            if (response == null)
            {
                return NoContent();
            }

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CompanyExpense>> GetCompanyExpenseById(int id)
        {
            var response = await _CompanyExpenseGroupService.GetCompanyExpenseById(id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<bool>> Create([FromBody] CompanyExpense CompanyExpenseModal)
        {
            return await _CompanyExpenseGroupService.Create(CompanyExpenseModal);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<bool>> Update(int id, [FromBody] CompanyExpense CompanyExpenseModal)
        {
            return await _CompanyExpenseGroupService.Update(id, CompanyExpenseModal);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            return await _CompanyExpenseGroupService.Delete(id);
        }
    }
}
