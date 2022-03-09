using PMS.Core.Interface.Services;
using PMS.Core.Model;
using Microsoft.AspNetCore.Mvc;

namespace PMS.API.Controllers
{
    [Route("employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        private readonly IEmployeeService _EmployeeService;

        public EmployeeController(IEmployeeService EmployeeService)
        {
            _EmployeeService = EmployeeService ?? throw new ArgumentNullException(nameof(EmployeeService));
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeListModel>>> GetAllEmployee()
        {
            var response = await _EmployeeService.GetAllEmployee();

            if (response == null)
            {
                return NoContent();
            }

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployeeById(int id)
        {
            var response = await _EmployeeService.GetEmployeeById(id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] Employee EmployeeModal)
        {
            return await _EmployeeService.Create(EmployeeModal);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<int>> Update(int id, [FromBody] Employee EmployeeModal)
        {
            return await _EmployeeService.Update(id, EmployeeModal);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> Delete(int id)
        {
            return await _EmployeeService.Delete(id);
        }
    }
}
