using PMS.Core.Interface.Services;
using PMS.Core.Model;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace PMS.API.Controllers
{
    [Route("dashboard")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _DashboardService;

        public DashboardController(IDashboardService DashboardService)
        {
            _DashboardService = DashboardService ?? throw new ArgumentNullException(nameof(DashboardService));
        }

        [HttpGet]
        public async Task<ActionResult<DashboardModal>> GetDashboardItems()
        {
            var response = await _DashboardService.GetDashboardItems();
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
    }
}
