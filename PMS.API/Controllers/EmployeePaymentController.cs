using PMS.Core.Interface.Services;
using PMS.Core.Model;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using PMS.Core.Services;

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
                return Ok(new
                {
                    message = "Server Error",
                    statusCode = HttpStatusCode.InternalServerError
                });
            }
            return Ok(new
            {
                message = "No records found",
                statusCode = HttpStatusCode.NoContent
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeePayment>> GetEmployeeById(int id)
        {
            var response = await _EmployeePaymentService.GetEmployeePaymentById(id);
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
        public async Task<ActionResult<int>> Create([FromBody] EmployeePayment EmployeePaymentModal)
        {
          var response = await _EmployeePaymentService.Create(EmployeePaymentModal);
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
                    message = "Employee id not found",
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
        public async Task<ActionResult<int>> Update(int id, [FromBody] EmployeePayment EmployeePaymentModal)
        {
         
            var response = await _EmployeePaymentService.Update(id, EmployeePaymentModal);
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
                    message = "Employee id not found",
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
            var response = await _EmployeePaymentService.Delete(id);
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

