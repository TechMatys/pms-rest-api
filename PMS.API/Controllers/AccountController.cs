using PMS.Core.Interface.Services;
using PMS.Core.Model;
using Microsoft.AspNetCore.Mvc;

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
            return Ok(response);
        }

      

        [HttpPatch("{id}")]
        public async Task<ActionResult<bool>> Update(int id, [FromBody] Account AccountModal)
        {
            return await _AccountService.Update(id, AccountModal);
        }

        
    }
}
