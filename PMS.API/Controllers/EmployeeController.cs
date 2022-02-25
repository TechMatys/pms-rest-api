using PMS.Core.Interface.Services;
using PMS.Core.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMS.API.Controllers
{
    [Route("employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        private readonly IEmployeeService _EmployeeGroupService;

        public EmployeeController(IEmployeeService EmployeeGroupService)
        {
            _EmployeeGroupService = EmployeeGroupService ?? throw new ArgumentNullException(nameof(EmployeeGroupService));
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAllEmployee()
        {
            var response = await _EmployeeGroupService.GetAllEmployee();

            if (response == null)
            {
                return NoContent();
            }

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployeeById(int id)
        {
            var response = await _EmployeeGroupService.GetEmployeeById(id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<bool>> Create([FromBody] Employee EmployeeModal)
        {
            return await _EmployeeGroupService.Create(EmployeeModal);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<bool>> Update(int id, [FromBody] Employee EmployeeModal)
        {
            return await _EmployeeGroupService.Update(id, EmployeeModal);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            return await _EmployeeGroupService.Delete(id);
        }
    }
}
