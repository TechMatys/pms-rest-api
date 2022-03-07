using PMS.Core.Interface.Services;
using PMS.Core.Model;
using Microsoft.AspNetCore.Mvc;

namespace PMS.API.Controllers
{
    [Route("users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _UsersService;

        public UsersController(IUsersService UsersService)
        {
            _UsersService = UsersService ?? throw new ArgumentNullException(nameof(UsersService));
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsersListModel>>> GetAllUsers()
        {
            var response = await _UsersService.GetAllUsers();

            if (response == null)
            {
                return NoContent();
            }

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Users>> GetUsersById(int id)
        {
            var response = await _UsersService.GetUsersById(id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<bool>> Create([FromBody] Users UsersModal)
        {
            return await _UsersService.Create(UsersModal);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<bool>> Update(int id, [FromBody] Users UsersModal)
        {
            return await _UsersService.Update(id, UsersModal);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            return await _UsersService.Delete(id);
        }
    }
}
