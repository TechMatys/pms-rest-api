using PMS.Core.Interface.Services;
using PMS.Core.Model;
using Microsoft.AspNetCore.Mvc;

namespace PMS.API.Controllers
{
    [Route("users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _UsersGroupService;

        public UsersController(IUsersService UsersGroupService)
        {
            _UsersGroupService = UsersGroupService ?? throw new ArgumentNullException(nameof(UsersGroupService));
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsersListModel>>> GetAllUsers()
        {
            var response = await _UsersGroupService.GetAllUsers();

            if (response == null)
            {
                return NoContent();
            }

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Users>> GetUsersById(int id)
        {
            var response = await _UsersGroupService.GetUsersById(id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<bool>> Create([FromBody] Users UsersModal)
        {
            return await _UsersGroupService.Create(UsersModal);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<bool>> Update(int id, [FromBody] Users UsersModal)
        {
            return await _UsersGroupService.Update(id, UsersModal);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            return await _UsersGroupService.Delete(id);
        }
    }
}
