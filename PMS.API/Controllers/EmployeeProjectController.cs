using PMS.Core.Interface.Services;
using PMS.Core.Model;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using PMS.Core.Services;

namespace PMS.API.Controllers
{
    [Route("employee-project")]
    [ApiController]
    public class EmployeeProjectController : ControllerBase
    {
        private readonly IEmployeeProjectService _EmployeeProjectService;

        public EmployeeProjectController(IEmployeeProjectService EmployeeProjectService)
        {
            _EmployeeProjectService = EmployeeProjectService ?? throw new ArgumentNullException(nameof(EmployeeProjectService));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeProjectListModel>>> GetAllEmployeeProject()
        {
            var response = await _EmployeeProjectService.GetAllEmployeeProject();

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
        public async Task<ActionResult<EmployeeProject>> GetEmployeeById(int id)
        {
            var response = await _EmployeeProjectService.GetEmployeeProjectById(id);
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
        public async Task<ActionResult<int>> Create([FromBody] EmployeeProject EmployeeProjectModal)
        {
         var response = await _EmployeeProjectService.Create(EmployeeProjectModal);
            if (response == null)
            {
                return Ok(new
                {
                    message = "Server Error",
                    StatusCode = HttpStatusCode.InternalServerError,
                });
            }
            else if (response == -1)
            {
                return Ok(new
                {
                    message = "Employee  does not exists",
                    StatusCode = HttpStatusCode.NotFound,
                });
            }
            else if (response == -2)
            {
                return Ok(new
                {
                    message = "Project does not exists",
                    StatusCode = HttpStatusCode.NotFound,
                });
            }
            else
                return Ok(new
            {
                response,
                message = "Created",
                statusCode = HttpStatusCode.OK,
            });
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<int>> Update(int id, [FromBody] EmployeeProject EmployeeProjectModal)
        {
         var response = await _EmployeeProjectService.Update(id, EmployeeProjectModal);
            if (response == null)
            {
                return Ok(new
                {
                    response,
                    message = "Server error",
                    statusCode = HttpStatusCode.InternalServerError,
                });
            }
            else if (response == -1)
            {
                return NotFound(new
                {
                    message = "Employee does not exists",
                    StatusCode = HttpStatusCode.NotFound,
                });
            }
            else if (response == -2)
            {
                return NotFound(new
                {
                    message = "Project does not exists",
                    StatusCode = HttpStatusCode.NotFound,
                });
            }
            else
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
         var response = await _EmployeeProjectService.Delete(id);
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
