using PMS.Core.Interface.Services;
using PMS.Core.Model;
using Microsoft.AspNetCore.Mvc;

namespace PMS.API.Controllers
{
    [Route("employee-payment")]
    [ApiController]
    public class EmployeePaymentController: ControllerBase
    {
        private readonly IEmployeePaymentService _EmployeePaymentService;

        public EmployeePaymentController(IEmployeePaymentService EmployeePaymentService)
        {
            _EmployeePaymentService = EmployeePaymentService ?? throw new ArgumentNullException(nameof(EmployeePaymentService));
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeePaymentListModel>>> GetAllEmployeePayment()
        {
            var response = await _EmployeePaymentService.GetAllEmployeePayment();

            if (response == null)
            {
                return NoContent();
            }

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeePayment>> GetEmployeeById(int id)
        {
            var response = await _EmployeePaymentService.GetEmployeePaymentById(id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<bool>> Create([FromBody] EmployeePayment EmployeePaymentModal)
        {
            return await _EmployeePaymentService.Create(EmployeePaymentModal);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<bool>> Update(int id, [FromBody] EmployeePayment EmployeePaymentModal)
        {
            return await _EmployeePaymentService.Update(id, EmployeePaymentModal);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            return await _EmployeePaymentService.Delete(id);
        }
    }
}

