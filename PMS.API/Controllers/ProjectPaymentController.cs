using PMS.Core.Interface.Services;
using PMS.Core.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMS.API.Controllers
{
    public class ProjectPaymentController: ControllerBase
    {
        private readonly IProjectPaymentService _ProjectPaymentGroupService;

        public ProjectPaymentController(IProjectPaymentService ProjectPaymentGroupService)
        {
            _ProjectPaymentGroupService = ProjectPaymentGroupService ?? throw new ArgumentNullException(nameof(ProjectPaymentGroupService));
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectPaymentListModel>>> GetAllProjectPayment()
        {
            var response = await _ProjectPaymentGroupService.GetAllProjectPayment();

            if (response == null)
            {
                return NoContent();
            }

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectPayment>> GetProjectPaymentById(int id)
        {
            var response = await _ProjectPaymentGroupService.GetProjectPaymentById(id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<bool>> Create([FromBody] ProjectPayment ProjectPaymentModal)
        {
            return await _ProjectPaymentGroupService.Create(ProjectPaymentModal);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<bool>> Update(int id, [FromBody] ProjectPayment ProjectPaymentModal)
        {
            return await _ProjectPaymentGroupService.Update(id, ProjectPaymentModal);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            return await _ProjectPaymentGroupService.Delete(id);
        }
    }
}
