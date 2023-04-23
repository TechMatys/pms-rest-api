using PMS.Core.Interface.Services;
using PMS.Core.Model;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using PMS.Core.Services;

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
                return Ok(new
                {
                    message = "Server Error",
                    statusCode = HttpStatusCode.InternalServerError
                });
            }
            return Ok(new
            {
                data = response,
                statusCode = HttpStatusCode.OK
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CompanyExpense>> GetCompanyExpenseById(int id)
        {
            var response = await _CompanyExpenseService.GetCompanyExpenseById(id);
            if (response == null)
            {
                return Ok(new
                {
                    data = response,
                    message = "No records found",
                    statusCode = HttpStatusCode.NotFound
                });
            }
            return Ok(new
            {
                data = response,
                statusCode = HttpStatusCode.OK
            });
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] CompanyExpense CompanyExpenseModal)
        {
         var response = await _CompanyExpenseService.Create(CompanyExpenseModal);
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
                data = response,
                message = "Created",
                statusCode = HttpStatusCode.OK,
            });
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<int>> Update(int id, [FromBody] CompanyExpense CompanyExpenseModal)
        {
            var response = await _CompanyExpenseService.Update(id, CompanyExpenseModal);
            if (response == null)
            {
                return Ok(new
                {
                    data = response,
                    message = "Server error",
                    statusCode = HttpStatusCode.InternalServerError,
                });
            }
            if (response == 0)
            {
                return Ok(new
                {
                    message = "No Records Found",
                    statusCode = HttpStatusCode.NotFound,
                });
            }
            return Ok(new
            {
                response,
                message = "Updated",
                statusCode = HttpStatusCode.OK,
            });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> Delete(int id)
        {
            var response = await _CompanyExpenseService.Delete(id);
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
