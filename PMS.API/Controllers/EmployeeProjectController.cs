using PMS.Core.Interface.Services;
using PMS.Core.Model;
using Microsoft.AspNetCore.Mvc;

namespace PMS.API.Controllers
{
    [Route("employee-project")]
    [ApiController]
    public class EmployeeProjectController : ControllerBase
    {
        private readonly IEmployeeProjectService _EmployeeProjectGroupService;

        public EmployeeProjectController(IEmployeeProjectService EmployeeProjectGroupService)
        {
            _EmployeeProjectGroupService = EmployeeProjectGroupService ?? throw new ArgumentNullException(nameof(EmployeeProjectGroupService));
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeProjectListModel>>> GetAllEmployeeProject()
        {
            var response = await _EmployeeProjectGroupService.GetAllEmployeeProject();

            if (response == null)
            {
                return NoContent();
            }

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeProject>> GetEmployeeById(int id)
        {
            var response = await _EmployeeProjectGroupService.GetEmployeeProjectById(id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<bool>> Create([FromBody] EmployeeProject EmployeeProjectModal)
        {
            return await _EmployeeProjectGroupService.Create(EmployeeProjectModal);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<bool>> Update(int id, [FromBody] EmployeeProject EmployeeProjectModal)
        {
            return await _EmployeeProjectGroupService.Update(id, EmployeeProjectModal);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            return await _EmployeeProjectGroupService.Delete(id);
        }
    }
}
