using PMS.Core.Interface.Services;
using PMS.Core.Model;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using PMS.Core.Services;

namespace PMS.API.Controllers
{
    [Route("account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _AccountService;

        public AccountController(IAccountService AccountService)
        {
            _AccountService = AccountService ?? throw new ArgumentNullException(nameof(AccountService));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> GetUserById(int id)
        {
            var response = await _AccountService.GetUserById(id);
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

        [HttpPatch("{id}")]
        public async Task<ActionResult<int>> Update(int id, [FromBody] Account AccountModal)
        {
            var response = await _AccountService.Update(id, AccountModal);
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
            return Ok(new
            {
                response,
                message = "Updated",
                statusCode = HttpStatusCode.OK,
            });
        }
    }
}


