using PMS.Core.Interface.Services;
using PMS.Core.Model;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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
            if (string.IsNullOrEmpty(EmployeeModal.FirstName))
            {
                return Ok(new
                {
                    message = "First Name shouldn't be blank",
                    statusCode = HttpStatusCode.BadRequest
                });
            }
            if (string.IsNullOrEmpty(EmployeeModal.EmailAddress))
            {
                return Ok(new
                {
                    message = "Email shouldn't be blank",
                    statusCode = HttpStatusCode.BadRequest
                });
            }

            var data = await _employeeService.Create(EmployeeModal);
            //If server error comes then execute this block
            if (data == null)
            {
                return Ok(new
                {
                    message = "Server Error",
                    StatusCode = HttpStatusCode.InternalServerError,
                });
            }
            // If any data already exists then execute this block
            if (data < 1)
            {
                return Ok(new
                {
                    message = "Email already exists",
                    statusCode = HttpStatusCode.Conflict,
                });
            }
            // Return id if record inserted successfully 
            return Ok(new
            {
                data,
                message = "Created",
                statusCode = HttpStatusCode.OK,
            });
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<int>> Update(int id, [FromBody] Employee EmployeeModal)
        {
            var data = await _employeeService.Update(id,EmployeeModal);
            if (id < 1)
            {
                return Ok(new
                {
                    message = "Invalid Id",
                    statusCode = HttpStatusCode.BadRequest
                });
            }
            if (string.IsNullOrEmpty(EmployeeModal.FirstName))
            {
                return Ok(new
                {
                    message = "First Name shouldn't be blank",
                    statusCode = HttpStatusCode.BadRequest
                });
            }
            if (string.IsNullOrEmpty(EmployeeModal.EmailAddress))
            {
                return Ok(new
                {
                    message = "Email shouldn't be blank",
                    statusCode = HttpStatusCode.BadRequest
                });
            }

            if (data == null)
            {
                return Ok(new
                {
                    data,
                    message = "Server error",
                    statusCode = HttpStatusCode.InternalServerError,
                });
            }
            // If any data already exists then execute this block
            if (data == 0)
            {
                return Ok(new
                {
                    message = "No Records Found",
                    statusCode = HttpStatusCode.NotFound,
                });
            }
            // If any data already exists then execute this block
            if (data < 1)
            {
                return Ok(new
                {
                    message = "Email Already Exists",
                    statusCode = HttpStatusCode.Conflict,
                });
            }
            // Return id if record updated successfully 
            return Ok(new
            {
                data,
                message = "Updated",
                statusCode = HttpStatusCode.OK,
            });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> Delete(int id)
        {
            if (id < 1)
            {
                return Ok(new
                {
                    message = "Invalid Id",
                    statusCode = HttpStatusCode.BadRequest,
                });
            }

            var data = await _employeeService.Delete(id);
            if (data == null)
            {
                return Ok(new
                {
                    message = "Server Error",
                    StatusCode = HttpStatusCode.InternalServerError,
                });
            }
            if (data < 1)
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
