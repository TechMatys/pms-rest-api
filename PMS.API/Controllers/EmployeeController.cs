using PMS.Core.Interface.Services;
using PMS.Core.Model;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using PMS.Core.Services;

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
    
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployeeById(int id)
        {
            var response = await _employeeService.GetEmployeeById(id);
                        
            if (response == null)
            {
                return Ok(new
                {
                    response,
                    message = "No records found",
                    statusCode = HttpStatusCode.NotFound
                });
            }
            return Ok(new
            {
                response,
                statusCode = HttpStatusCode.OK
            });
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] Employee EmployeeModal)
        {
        var response = await _employeeService.Create(EmployeeModal);
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
                    message = "Email already exists",
                    statusCode = HttpStatusCode.Conflict,
                });
            }
            return Ok(new
            {
                response,
                message = "Created",
                statusCode = HttpStatusCode.OK,
            });
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<int>> Update(int id, [FromBody] Employee EmployeeModal)
        {
            var response = await _employeeService.Update(id,EmployeeModal);
        if (response == null)
            {
                return Ok(new
                {
                    response,
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
            if (response < 1)
            {
                return Ok(new
                {
                    message = "Email Already Exists",
                    statusCode = HttpStatusCode.Conflict,
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
            var response = await _employeeService.Delete(id);
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

   #region Employee Task

        [HttpGet("{id}/task")]
        public async Task<ActionResult<IEnumerable<EmployeeTaskListModel>>> GetAllTaskDetails(int id)
        {
            var response = await _employeeService.GetAllTaskDetails(id);

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

        [HttpGet("{id}/task/{taskId}")]
        public async Task<ActionResult<EmployeeTaskDetails>> GetTaskDetailById(int id, int taskId)
        {
            var response = await _employeeService.GetTaskDetailById(id, taskId);
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

        [HttpPost("{id}/task")]
        public async Task<ActionResult<int>> CreateTask(int id, [FromBody] EmployeeTaskDetails EmployeeModal)
        {
            var response=await _employeeService.CreateTask(id, EmployeeModal);
            
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

        [HttpDelete("{id}/task/{taskId}")]
        public async Task<ActionResult<int>> DeleteTask(int id, int taskId)
        {
            var response = await _employeeService.Delete(id);
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
   #endregion
   }
}
