using PMS.Core.Interface.Services;
using PMS.Core.Model;
using Microsoft.AspNetCore.Mvc;

namespace PMS.API.Controllers
{
    [Route("project")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _ProjectService;

        public ProjectController(IProjectService ProjectService)
        {
            _ProjectService = ProjectService ?? throw new ArgumentNullException(nameof(ProjectService));
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectListModel>>> GetAllProject()
        {
            var response = await _ProjectService.GetAllProject();

            if (response == null)
            {
                return NoContent();
            }

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProjectById(int id)
        {
            var response = await _ProjectService.GetProjectById(id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] Project ProjectModal)
        {
            return await _ProjectService.Create(ProjectModal);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<int>> Update(int id, [FromBody] Project ProjectModal)
        {
            return await _ProjectService.Update(id, ProjectModal);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> Delete(int id)
        {
            return await _ProjectService.Delete(id);
        }
    }
}
