using PMS.Core.Interface.Services;
using PMS.Core.Model;
using Microsoft.AspNetCore.Mvc;

namespace PMS.API.Controllers
{
    [Route("employee-payment")]
    [ApiController]
    public class EmployeePaymentController: ControllerBase
    {
        private readonly IEmployeePaymentService _EmployeePaymentGroupService;

        public EmployeePaymentController(IEmployeePaymentService EmployeePaymentGroupService)
        {
            _EmployeePaymentGroupService = EmployeePaymentGroupService ?? throw new ArgumentNullException(nameof(EmployeePaymentGroupService));
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeePaymentListModel>>> GetAllEmployeePayment()
        {
            var response = await _EmployeePaymentGroupService.GetAllEmployeePayment();

            if (response == null)
            {
                return NoContent();
            }

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeePayment>> GetEmployeeById(int id)
        {
            var response = await _EmployeePaymentGroupService.GetEmployeePaymentById(id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<bool>> Create([FromBody] EmployeePayment EmployeePaymentModal)
        {
            return await _EmployeePaymentGroupService.Create(EmployeePaymentModal);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<bool>> Update(int id, [FromBody] EmployeePayment EmployeePaymentModal)
        {
            return await _EmployeePaymentGroupService.Update(id, EmployeePaymentModal);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            return await _EmployeePaymentGroupService.Delete(id);
        }
    }
}

