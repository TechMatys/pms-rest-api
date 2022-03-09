using PMS.Core.Interface.Services;
using PMS.Core.Model;
using Microsoft.AspNetCore.Mvc;

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
                return NoContent();
            }

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectPayment>> GetProjectPaymentById(int id)
        {
            var response = await _ProjectPaymentService.GetProjectPaymentById(id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] ProjectPayment ProjectPaymentModal)
        {
            return await _ProjectPaymentService.Create(ProjectPaymentModal);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<int>> Update(int id, [FromBody] ProjectPayment ProjectPaymentModal)
        {
            return await _ProjectPaymentService.Update(id, ProjectPaymentModal);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> Delete(int id)
        {
            return await _ProjectPaymentService.Delete(id);
        }
    }
}
