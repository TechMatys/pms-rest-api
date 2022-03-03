using PMS.Core.Interface.Services;
using PMS.Core.Model;
using Microsoft.AspNetCore.Mvc;

namespace PMS.API.Controllers
{
    [Route("project")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _ProjectGroupService;

        public object? ProjectGroupService { get; }

        public ProjectController(IProjectService ProjectGroupService)
        {
            _ProjectGroupService = ProjectGroupService ?? throw new ArgumentNullException(nameof(ProjectGroupService));
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectListModel>>> GetAllProject()
        {
            var response = await _ProjectGroupService.GetAllProject();

            if (response == null)
            {
                return NoContent();
            }

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProjectById(int id)
        {
            var response = await _ProjectGroupService.GetProjectById(id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<bool>> Create([FromBody] Project ProjectModal)
        {
            return await _ProjectGroupService.Create(ProjectModal);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<bool>> Update(int id, [FromBody] Project ProjectModal)
        {
            return await _ProjectGroupService.Update(id, ProjectModal);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            return await _ProjectGroupService.Delete(id);
        }
    }
}
