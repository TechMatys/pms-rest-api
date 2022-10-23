using PMS.Core.Interface.Services;
using PMS.Core.Model;
using Microsoft.AspNetCore.Mvc;

namespace PMS.API.Controllers
{
    [Route("employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService EmployeeService)
        {
            _employeeService = EmployeeService ?? throw new ArgumentNullException(nameof(EmployeeService));
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeListModel>>> GetAllEmployee()
        {
            var response = await _employeeService.GetAllEmployee();

            if (response == null)
            {
                return NoContent();
            }

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployeeById(int id)
        {
            var response = await _employeeService.GetEmployeeById(id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] Employee EmployeeModal)
        {
            return await _employeeService.Create(EmployeeModal);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<int>> Update(int id, [FromBody] Employee EmployeeModal)
        {
            return await _employeeService.Update(id, EmployeeModal);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> Delete(int id)
        {
            return await _employeeService.Delete(id);
        }

        #region Employee Task

        [HttpGet("{id}/task")]
        public async Task<ActionResult<IEnumerable<EmployeeTaskListModel>>> GetAllTaskDetails(int id)
        {
            var response = await _employeeService.GetAllTaskDetails(id);

            if (response == null)
            {
                return NoContent();
            }

            return Ok(response);
        }

        [HttpGet("{id}/task/{taskId}")]
        public async Task<ActionResult<EmployeeTaskDetails>> GetTaskDetailById(int id, int taskId)
        {
            var response = await _employeeService.GetTaskDetailById(id, taskId);
            return Ok(response);
        }

        [HttpPost("{id}/task")]
        public async Task<ActionResult<int>> CreateTask(int id, [FromBody] EmployeeTaskDetails EmployeeModal)
        {
            return await _employeeService.CreateTask(id, EmployeeModal);
        }

        [HttpDelete("{id}/task/{taskId}")]
        public async Task<ActionResult<int>> DeleteTask(int id, int taskId)
        {
            return await _employeeService.DeleteTask(id, taskId);
        }

        #endregion

    }
}
