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

        [HttpGet("states")]
        public async Task<ActionResult<IEnumerable<GlobalCodes>>> GetAllStates()
        {
            var response = await _GlobalCodeService.GetAllStates();

            if (response == null)
            {
                return NoContent();
            }

            return Ok(response);
        }

    }
}
