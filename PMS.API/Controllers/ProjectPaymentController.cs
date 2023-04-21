using PMS.Core.Interface.Services;
using PMS.Core.Model;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using PMS.Core.Services;

namespace PMS.API.Controllers
{
    [Route("project-payment")]
    [ApiController]
    public class ProjectPaymentController: ControllerBase
    {
        private readonly IProjectPaymentService _ProjectPaymentService;

        public ProjectPaymentController(IProjectPaymentService ProjectPaymentService)
        {
            _ProjectPaymentService = ProjectPaymentService ?? throw new ArgumentNullException(nameof(ProjectPaymentService));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectPaymentListModel>>> GetAllProjectPayment()
        {
            var response = await _ProjectPaymentService.GetAllProjectPayment();
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
        public async Task<ActionResult<ProjectPayment>> GetProjectPaymentById(int id)
        {
            var response = await _ProjectPaymentService.GetProjectPaymentById(id);
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
        public async Task<ActionResult<int>> Create([FromBody] ProjectPayment ProjectPaymentModal)
        {
            var response = await _ProjectPaymentService.Create(ProjectPaymentModal);
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
                    message = "Project id not found",
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
        public async Task<ActionResult<int>> Update(int id, [FromBody] ProjectPayment ProjectPaymentModal)
        {
            var response = await _ProjectPaymentService.Update(id, ProjectPaymentModal);
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
                    message = "Project id not found",
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
            var response = await _ProjectPaymentService.Delete(id);
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
