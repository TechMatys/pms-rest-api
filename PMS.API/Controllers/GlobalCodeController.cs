using Microsoft.AspNetCore.Mvc;
using PMS.Core.Interface.Services;
using PMS.Core.Model;

namespace PMS.API.Controllers
{
    [Route("global-codes")]
    [ApiController]
    public class GlobalCodeController : ControllerBase
    {
        private readonly IGlobalCodeService _GlobalCodeService;

        public GlobalCodeController(IGlobalCodeService GlobalCodeService)
        {
            _GlobalCodeService = GlobalCodeService ?? throw new ArgumentNullException(nameof(GlobalCodeService));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GlobalCodes>>> GetAllStates()
        {
            var response = await _GlobalCodeService.GetAllStates();

            if (response == null)
            {
                return NoContent();
            }

            return Ok(response);
        }

        [HttpGet("designations")]
        public async Task<ActionResult<IEnumerable<GlobalCodes>>> GetAllDesignations()
        {
            string category = "Designation";
            var response = await _GlobalCodeService.GetAllGlobalCodes(category);

            if (response == null)
            {
                return NoContent();
            }

            return Ok(response);
        }

        [HttpGet("employee-status")]
        public async Task<ActionResult<IEnumerable<GlobalCodes>>> GetAllEmployeeStatus()
        {
            string category = "EmployeeStatus";
            var response = await _GlobalCodeService.GetAllGlobalCodes(category);

            if (response == null)
            {
                return NoContent();
            }

            return Ok(response);
        }

        [HttpGet("project-status")]
        public async Task<ActionResult<IEnumerable<GlobalCodes>>> GetAllProjectStatus()
        {
            string category = "ProjectStatus";
            var response = await _GlobalCodeService.GetAllGlobalCodes(category);

            if (response == null)
            {
                return NoContent();
            }

            return Ok(response);
        }

        [HttpGet("project-durations")]
        public async Task<ActionResult<IEnumerable<GlobalCodes>>> GetAllProjectDurations()
        {
            string category = "ProjectDuration";
            var response = await _GlobalCodeService.GetAllGlobalCodes(category);

            if (response == null)
            {
                return NoContent();
            }

            return Ok(response);
        }

        [HttpGet("technologies")]
        public async Task<ActionResult<IEnumerable<GlobalCodes>>> GetAllTechnologies()
        {
            string category = "ProjectTechnology";
            var response = await _GlobalCodeService.GetAllGlobalCodes(category);

            if (response == null)
            {
                return NoContent();
            }

            return Ok(response);
        }

    }
}
